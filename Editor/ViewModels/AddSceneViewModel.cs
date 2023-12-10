using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using AvaloniaEditor.Models;
using FishStick.Scene;
using ReactiveUI;


namespace AvaloniaEditor.ViewModels
{
  public class AddSceneViewModel : ViewModelBase
  {

    private string _description = string.Empty;
    private string _name = string.Empty;

    private ObservableCollection<ITransition> _transitions = new ObservableCollection<ITransition>();

    public ReactiveCommand<Unit, SceneModel> CreateCommand { get; }
    public ReactiveCommand<Unit, Unit> CancelCommand { get; }

    public ReactiveCommand<ITransition, Unit> RemoveTransitionCommand { get; }

    public ICommand AddTransitionCommand { get; }

    public Interaction<AddSceneTransitionViewModel, ITransition?> ShowDialog { get; }
    public AddSceneViewModel(List<SceneModel> availableScenes)
    {
      ShowDialog = new Interaction<AddSceneTransitionViewModel, ITransition?>();
      var isValidObservable = this.WhenAnyValue(
          viewModel => viewModel.Name,
          viewModel => viewModel.Description,
          (name, description) => !string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(description));

      CreateCommand = ReactiveCommand.Create(
          () => new SceneModel { Description = Description, Name = Name }, isValidObservable);
      AddTransitionCommand = ReactiveCommand.CreateFromTask(async () =>
      {
        var addTransitionVm = new AddSceneTransitionViewModel(availableScenes);
        var result = await ShowDialog.Handle(addTransitionVm);
        if (result != null)
          Transitions.Add(result);
      });
      CancelCommand = ReactiveCommand.Create(() => { });
      RemoveTransitionCommand = ReactiveCommand.Create<ITransition>(transition => Transitions.Remove(transition));
    }


    public ObservableCollection<ITransition> Transitions
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