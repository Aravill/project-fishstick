using ReactiveUI;
using AvaloniaEditor.Services;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;

namespace AvaloniaEditor.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
  private ViewModelBase _contentViewModel;
  private SceneService _sceneService;
  public SceneViewModel SceneViewModel { get; }


  public MainWindowViewModel()
  {
    _sceneService = SceneService.Instance;
    SceneViewModel = new SceneViewModel(this);
    _contentViewModel = SceneViewModel;

    if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
    {
      desktopLifetime.Exit += OnExit;
    }
  }

  public ViewModelBase ContentViewModel
  {
    get => _contentViewModel;
    private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
  }

  private void OnExit(object? sender, ControlledApplicationLifetimeExitEventArgs e)
  {
    _sceneService.SaveScenes();
  }

  public void SwitchToView(ViewModelBase viewModel)
  {
    ContentViewModel = viewModel;
  }

}
