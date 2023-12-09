using System.Reactive;
using AvaloniaEditor.Models;
using ReactiveUI;

namespace AvaloniaEditor.ViewModels
{
  public class AddSceneViewModel : ViewModelBase
  {
    private string _description = string.Empty;


    public ReactiveCommand<Unit, SceneModel> OkCommand { get; }
    public ReactiveCommand<Unit, Unit> CancelCommand { get; }
    public AddSceneViewModel()
    {
      var isValidObservable = this.WhenAnyValue(
        viewModel => viewModel.Description,
        descriptionValue => !string.IsNullOrWhiteSpace(descriptionValue));

      OkCommand = ReactiveCommand.Create(
          () => new SceneModel { Description = Description }, isValidObservable);
      CancelCommand = ReactiveCommand.Create(() => { });
    }

    public string Description
    {
      get => _description;
      set => this.RaiseAndSetIfChanged(ref _description, value);
    }
  }
}