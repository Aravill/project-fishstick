﻿using ReactiveUI;
using System;
using System.Reactive.Linq;
using AvaloniaEditor.Models;
using AvaloniaEditor.Services;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using System.Linq;

namespace AvaloniaEditor.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
  private ViewModelBase _contentViewModel;
  private SceneService _sceneService;
  public SceneViewModel ScenesView { get; }


  public MainWindowViewModel()
  {
    _sceneService = new SceneService();
    var scenes = _sceneService.GetItems();

    ScenesView = new SceneViewModel(scenes);
    _contentViewModel = ScenesView;

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

  public void AddScene()
  {
    AddSceneViewModel addSceneViewModel = new(_sceneService.GetItems().ToList());

    Observable.Merge(
        addSceneViewModel.CreateCommand,
        addSceneViewModel.CancelCommand.Select(_ => (SceneModel?)null))
        .Take(1)
        .Subscribe(newScene =>
        {
          if (newScene != null)
          {
            _sceneService.AddItem(newScene);
            ScenesView.Scenes.Add(newScene);
          }
          ContentViewModel = ScenesView;
        });

    ContentViewModel = addSceneViewModel;
  }

}
