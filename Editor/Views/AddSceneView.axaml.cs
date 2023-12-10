using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.VisualTree;
using AvaloniaEditor.ViewModels;
using FishStick.Scene;
using ReactiveUI;

namespace AvaloniaEditor.Views;

public partial class AddSceneView : UserControl
{
  public AddSceneView()
  {
    InitializeComponent();
    DataContextChanged += OnDataContextChanged;
  }

  private void OnDataContextChanged(object? sender, EventArgs e)
  {
    if (DataContext is AddSceneViewModel vm)
      vm.ShowDialog.RegisterHandler(DoShowDialogAsync);
  }

  private async Task DoShowDialogAsync(InteractionContext<AddSceneTransitionViewModel, ITransition?> context)
  {
    var dialog = new AddSceneTransitionView
    {
      DataContext = context.Input
    };

    var window = this.GetVisualRoot() as Window;
    if (window != null)
    {
      var result = await dialog.ShowDialog<ITransition?>(window);
      context.SetOutput(result);
    }
  }
}