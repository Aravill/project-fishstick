using ReactiveUI;
using System;
using System.Reactive.Linq;
using AvaloniaEditor.Models;
using AvaloniaEditor.Services;

namespace AvaloniaEditor.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
  private ViewModelBase _contentViewModel;
  public SceneViewModel ScenesView { get; }

  public MainWindowViewModel()
  {
    var service = new SceneService();
    ScenesView = new SceneViewModel(service.GetItems());
    _contentViewModel = ScenesView;
  }

  public ViewModelBase ContentViewModel
  {
    get => _contentViewModel;
    private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
  }

  public void AddScene()
  {
    AddSceneViewModel addItemViewModel = new();

    Observable.Merge(
        addItemViewModel.OkCommand,
        addItemViewModel.CancelCommand.Select(_ => (Scene?)null))
        .Take(1)
        .Subscribe(newScene =>
        {
          if (newScene != null)
          {
            ScenesView.ListScenes.Add(newScene);
          }
          ContentViewModel = ScenesView;
        });

    ContentViewModel = addItemViewModel;
  }


}
