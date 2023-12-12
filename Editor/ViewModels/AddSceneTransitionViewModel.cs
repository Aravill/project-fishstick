using System.Reactive.Linq;
using System.Collections.Generic;
using System.Reactive;
using AvaloniaEditor.Models;
using FishStick.Scene;
using ReactiveUI;
using Avalonia.Data;

namespace AvaloniaEditor.ViewModels
{
  public class AddSceneTransitionViewModel : ViewModelBase
  {

    private string _description = string.Empty;
    private string _name = string.Empty;
    private SceneModel? _selectedScene = null;

    public ReactiveCommand<Unit, BaseTransition> CreateCommand { get; }
    public ReactiveCommand<Unit, Unit> CancelCommand { get; }

    private List<SceneModel> _scenes { get; set; }
    private List<BaseTransition> _transitions { get; set; }
    public Interaction<BaseTransition?, Unit> CloseDialog { get; } = new Interaction<BaseTransition?, Unit>();
    public AddSceneTransitionViewModel(List<SceneModel> availableScenes, List<BaseTransition> availableTransitions)
    {
      _scenes = availableScenes;
      _transitions = availableTransitions;
      var isValidObservable = this.WhenAnyValue(
          viewModel => viewModel.Name,
          viewModel => viewModel.Description,
          viewModel => viewModel.SelectedScene,
          (name, description, targetSceneId) => !TransitionExists(name) && !string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(description) && SelectedScene != null);
      CreateCommand = ReactiveCommand.CreateFromTask(
           async () =>
           {
             var transition = new BaseTransition(Name, Description, SelectedScene!.Id);
             CreatedTransition = transition;
             await CloseDialog.Handle(transition);
             return transition;
           }, isValidObservable);
      CancelCommand = ReactiveCommand.CreateFromTask(async () =>
      {
        await CloseDialog.Handle(null);
      });
    }

    public string Description
    {
      get => _description;
      set => this.RaiseAndSetIfChanged(ref _description, value);
    }

    public string Name
    {
      get => _name;
      set
      {
        if (TransitionExists(value))
        {
          throw new DataValidationException("Transition with this name already exists");
        }
        this.RaiseAndSetIfChanged(ref _name, value);
      }
    }

    public IEnumerable<string> SceneNames
    {
      get => _scenes.ConvertAll(scene => scene.Name);
    }

    public IEnumerable<SceneModel> Scenes => _scenes;

    public SceneModel? SelectedScene
    {
      get => _selectedScene;
      set => this.RaiseAndSetIfChanged(ref _selectedScene, value);
    }

    public BaseTransition? CreatedTransition { get; set; }

    private bool TransitionExists(string transitionName)
    {
      return _transitions.Find(t => t.Name == transitionName) != null;
    }

  }

}