using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using AvaloniaEditor.Models;
using AvaloniaEditor.Services;
using FishStick.Scene;
using ReactiveUI;

namespace AvaloniaEditor.ViewModels
{
  public class AddSceneViewModel : ViewModelBase
  {

    private SceneModel _scene = new SceneModel();
    private string _description = string.Empty;
    private string _name = string.Empty;

    private List<SceneModel> _availableScenes;
    private ObservableCollection<PairModel<BaseTransition, string>> _transitions = new ObservableCollection<PairModel<BaseTransition, string>>();

    public ReactiveCommand<Unit, SceneModel> OkCommand { get; }
    public ReactiveCommand<Unit, Unit> CancelCommand { get; }

    public ReactiveCommand<PairModel<BaseTransition, string>, Unit> RemoveTransitionCommand { get; }

    public ICommand AddTransitionCommand { get; }

    public Interaction<AddSceneTransitionViewModel, BaseTransition?> ShowDialog { get; }
    public AddSceneViewModel()
    {
      _availableScenes = SceneService.Instance.GetScenes().ToList();
      ShowDialog = new Interaction<AddSceneTransitionViewModel, BaseTransition?>();
      var isValidObservable = this.WhenAnyValue(
          viewModel => viewModel.Name,
          viewModel => viewModel.Description,
          (name, description) => !string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(description));

      OkCommand = ReactiveCommand.Create(
          () =>
          {
            _scene.Name = Name;
            _scene.Description = Description;
            _scene.Transitions = Transitions.Select(t => t.First).ToList();
            return _scene;
          }, isValidObservable);

      AddTransitionCommand = ReactiveCommand.CreateFromTask(async () =>
      {
        var addTransitionVm = new AddSceneTransitionViewModel(_availableScenes, _transitions.Select(t => t.First).ToList());
        var result = await ShowDialog.Handle(addTransitionVm);
        if (result != null)
          Transitions.Add(new PairModel<BaseTransition, string>(result, FindSceneName(result.NextSceneId) ?? string.Empty));
      });

      CancelCommand = ReactiveCommand.Create(() => { });

      RemoveTransitionCommand = ReactiveCommand.Create<PairModel<BaseTransition, string>>(
        RemoveTransition);
    }

    public void InitializeFromExisting(SceneModel scene)
    {
      _scene = scene;
      Transitions = new ObservableCollection<PairModel<BaseTransition, string>>();
      foreach (var transition in scene.Transitions)
      {
        Transitions.Add(new PairModel<BaseTransition, string>(transition, FindSceneName(transition.NextSceneId) ?? string.Empty));
      }
      Name = scene.Name;
      Description = scene.Description;
    }

    private void RemoveTransition(PairModel<BaseTransition, string> transitionPair)
    {
      Transitions.Remove(Transitions.First(t => t == transitionPair));
    }

    private string? FindSceneName(string sceneId)
    {
      return _availableScenes.FirstOrDefault(scene => scene.Id == sceneId)?.Name;
    }
    public ObservableCollection<PairModel<BaseTransition, string>> Transitions
    {
      get => _transitions;
      set => this.RaiseAndSetIfChanged(ref _transitions, value);
    }

    public string Description
    {
      get => _description;
      set => this.RaiseAndSetIfChanged(ref _description, value);
    }

    public string Name
    {
      get => _name;
      set => this.RaiseAndSetIfChanged(ref _name, value);
    }

  }
}