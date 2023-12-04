using AvaloniaEditor.Services;

namespace AvaloniaEditor.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
  public MainWindowViewModel()
  {
    var service = new SceneService();
    Scenes = new SceneViewModel(service.GetItems());
  }

  public SceneViewModel Scenes { get; }
}
