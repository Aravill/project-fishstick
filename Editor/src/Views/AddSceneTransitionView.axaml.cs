using System;
using System.Reactive;
using Avalonia.Controls;
using AvaloniaEditor.ViewModels;
using FishStick.Scene;
using ReactiveUI;

namespace AvaloniaEditor.Views;

public partial class AddSceneTransitionView : Window
{
  public AddSceneTransitionView()
  {
    InitializeComponent();
    DataContextChanged += OnDataContextChanged;
  }

  private void OnDataContextChanged(object? sender, EventArgs e)
  {
    if (DataContext is AddSceneTransitionViewModel vm)
      vm.CloseDialog.RegisterHandler(CloseDialog);
  }

  private void CloseDialog(InteractionContext<BaseTransition?, Unit> context)
  {
    Close(context.Input);
  }
}