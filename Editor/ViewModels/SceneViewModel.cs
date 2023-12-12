using System.Collections.ObjectModel;
using System.Reactive.Linq;
using AvaloniaEditor.Models;
using AvaloniaEditor.Services;
using ReactiveUI;
using System;
using System.Reactive;
using System.Linq;

namespace AvaloniaEditor.ViewModels
{
  public class SceneViewModel : ViewModelBase
  {

    private ViewModelBase _contentViewModel;
    private SceneService _sceneService;

    private MainWindowViewModel _mainWindow;

    public ReactiveCommand<SceneModel, Unit> EditSceneCommand { get; }
    public ReactiveCommand<SceneModel, Unit> RemoveSceneCommand { get; }
    public ReactiveCommand<Unit, Unit> AddSceneCommand { get; }
    public SceneViewModel(MainWindowViewModel mainWindow)
    {
      _mainWindow = mainWindow;
      _sceneService = SceneService.Instance;
      _contentViewModel = this;
      Scenes = new ObservableCollection<SceneModel>(_sceneService.GetScenes());

      EditSceneCommand = ReactiveCommand.Create<SceneModel>(EditScene);
      RemoveSceneCommand = ReactiveCommand.Create<SceneModel>(RemoveScene);
      AddSceneCommand = ReactiveCommand.Create(AddScene);
    }

    public ObservableCollection<SceneModel> Scenes { get; }

    public void UpdateScene(SceneModel scene)
    {
      // TODO: Maybe this won't be necessary, since updating is changing the object, not the reference
      int index = Scenes.IndexOf(scene);
      if (index != -1)
      {
        Scenes[index] = scene;
      }
    }

    public void RemoveScene(SceneModel scene)
    {
      _sceneService.RemoveScene(scene);
      Scenes.Remove(scene);
    }

    public void EditScene(SceneModel scene)
    {
      AddSceneViewModel addSceneViewModel = new();
      addSceneViewModel.InitializeFromExisting(scene);

      Observable.Merge(
        addSceneViewModel.OkCommand,
        addSceneViewModel.CancelCommand.Select(_ => (SceneModel?)null))
        .Take(1)
        .Subscribe(editedScene =>
        {
          if (editedScene != null)
          {
            _sceneService.UpdateScene(editedScene);
            UpdateScene(editedScene);
          }
          _mainWindow.SwitchToView(this);
        });


      _mainWindow.SwitchToView(addSceneViewModel);
    }
    public void AddScene()
    {
      AddSceneViewModel addSceneViewModel = new();

      Observable.Merge(
          addSceneViewModel.OkCommand,
          addSceneViewModel.CancelCommand.Select(_ => (SceneModel?)null))
          .Take(1)
          .Subscribe(newScene =>
          {
            if (newScene != null)
            {
              _sceneService.AddScene(newScene);
              Scenes.Add(newScene);
            }
            _mainWindow.SwitchToView(this);
          });

      _mainWindow.SwitchToView(addSceneViewModel);
    }

    public ViewModelBase ContentViewModel
    {
      get => _contentViewModel;
      private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
    }

  }


}