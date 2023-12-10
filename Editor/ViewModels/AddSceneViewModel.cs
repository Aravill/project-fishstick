using System.Reactive;
using AvaloniaEditor.Models;
using ReactiveUI;

namespace AvaloniaEditor.ViewModels
{
  public class AddSceneViewModel : ViewModelBase
  {
    private string _description = string.Empty;
    private string _name = string.Empty;


    public ReactiveCommand<Unit, SceneModel> OkCommand { get; }
    public ReactiveCommand<Unit, Unit> CancelCommand { get; }
    public AddSceneViewModel()
    {
      var isValidObservable = this.WhenAnyValue(
          viewModel => viewModel.Name,
          viewModel => viewModel.Description,
          (name, description) => !string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(description));

      OkCommand = ReactiveCommand.Create(
          () => new SceneModel { Description = Description, Name = Name }, isValidObservable);
      CancelCommand = ReactiveCommand.Create(() => { });
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