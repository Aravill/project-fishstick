using System.Collections.ObjectModel;
using System.Reactive.Linq;
using AvaloniaEditor.Models;
using AvaloniaEditor.Services;
using ReactiveUI;
using System;
using System.Reactive;
using System.Linq;
using System.Windows.Input;

namespace AvaloniaEditor.ViewModels
{
  public class SceneViewModel : ViewModelBase
  {

    private SceneModel? _selectedScene;
    private ViewModelBase _contentViewModel;
    private SceneService _sceneService;

    private MainWindowViewModel _mainWindow;

    public ReactiveCommand<SceneModel, Unit> EditSceneCommand { get; }
    public ReactiveCommand<Unit, Unit> AddSceneCommand { get; }

    public ICommand RemoveSceneCommand { get; }
    public Interaction<YesNoViewModel, bool> ShowDialog { get; }
    public SceneViewModel(MainWindowViewModel mainWindow)
    {
      _mainWindow = mainWindow;
      _sceneService = SceneService.Instance;
      _contentViewModel = this;
      Scenes = new ObservableCollection<SceneModel>(_sceneService.GetScenes());

      EditSceneCommand = ReactiveCommand.Create<SceneModel>(EditScene);

      ShowDialog = new Interaction<YesNoViewModel, bool>();

      RemoveSceneCommand = ReactiveCommand.CreateFromTask<SceneModel>(async (scene) =>
      {
        var yesNoVm = new YesNoViewModel() { Message = $"Are you sure you want to remove scene {scene.Name}?" };
        var result = await ShowDialog.Handle(yesNoVm);
        if (result)
          RemoveScene(scene);
      });

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

    public SceneModel? SelectedScene
    {
      get => _selectedScene;
      set
      {
        this.RaiseAndSetIfChanged(ref _selectedScene, value);
      }
    }

    public void SelectSceneById(string sceneId)
    {
      SelectedScene = Scenes.FirstOrDefault(s => s.Id == sceneId);
    }

    public void DeselectScene()
    {
      SelectedScene = null;
    }
  }
}