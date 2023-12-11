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
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ReactiveUI;

namespace AvaloniaEditor.ViewModels
{
  public class AddSceneViewModel : ViewModelBase
  {

    private string _description = string.Empty;
    private string _name = string.Empty;

    private List<SceneModel> _availableScenes;
    private ObservableCollection<PairModel<ITransition, string>> _transitions = new ObservableCollection<PairModel<ITransition, string>>();

    public ReactiveCommand<Unit, SceneModel> CreateCommand { get; }
    public ReactiveCommand<Unit, Unit> CancelCommand { get; }

    public ReactiveCommand<PairModel<ITransition, string>, Unit> RemoveTransitionCommand { get; }

    public ICommand AddTransitionCommand { get; }

    public Interaction<AddSceneTransitionViewModel, ITransition?> ShowDialog { get; }
    public AddSceneViewModel()
    {
      _availableScenes = SceneService.Instance.GetItems().ToList();
      ShowDialog = new Interaction<AddSceneTransitionViewModel, ITransition?>();
      var isValidObservable = this.WhenAnyValue(
          viewModel => viewModel.Name,
          viewModel => viewModel.Description,
          (name, description) => !string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(description));

      CreateCommand = ReactiveCommand.Create(
          () => new SceneModel { Description = Description, Name = Name }, isValidObservable);

      AddTransitionCommand = ReactiveCommand.CreateFromTask(async () =>
      {
        var addTransitionVm = new AddSceneTransitionViewModel(_availableScenes);
        var result = await ShowDialog.Handle(addTransitionVm);
        if (result != null)
          Transitions.Add(new PairModel<ITransition, string>(result, FindSceneName(result.NextSceneId) ?? string.Empty));
      });

      CancelCommand = ReactiveCommand.Create(() => { });

      RemoveTransitionCommand = ReactiveCommand.Create<PairModel<ITransition, string>>(
        RemoveTransition);
    }

    private void RemoveTransition(PairModel<ITransition, string> transitionPair)
    {
      Transitions.Remove(Transitions.First(t => t == transitionPair));
    }

    private string? FindSceneName(string sceneId)
    {
      return _availableScenes.FirstOrDefault(scene => scene.Id == sceneId)?.Name;
    }
    public ObservableCollection<PairModel<ITransition, string>> Transitions
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