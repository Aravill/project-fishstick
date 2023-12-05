using AvaloniaEditor.Services;
using ReactiveUI;

namespace AvaloniaEditor.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
  private ViewModelBase _contentViewModel;
  public SceneViewModel Scenes { get; }
  public MainWindowViewModel()
  {
    var service = new SceneService();
    Scenes = new SceneViewModel(service.GetItems());
    _contentViewModel = Scenes;
  }

  public ViewModelBase ContentViewModel
  {
    get => _contentViewModel;
    private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
  }

  public void AddScene()
  {
    ContentViewModel = new AddSceneViewModel();
  }


}
