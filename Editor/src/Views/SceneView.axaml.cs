
using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.VisualTree;
using AvaloniaEditor.Controls;
using AvaloniaEditor.Helpers;
using AvaloniaEditor.ViewModels;
using ReactiveUI;

namespace AvaloniaEditor.Views;

public partial class SceneView : UserControl
{

  private ScenePanel? _selectedScenePanel { get; set; }
  public SceneView()
  {
    InitializeComponent();
    DataContextChanged += OnDataContextChanged;
  }

  public void Canvas_PointerPressed(object sender, PointerPressedEventArgs e)
  {
    // Wherever the user clicks, we deselect anyway
    DeselectScenePanel();
    // Check if the cursor was over a scene panel
    Control? control = (Control?)e.Source;
    if (control == null)
      return;
    ScenePanel? scenePanel = ControlUtils.FindParentOfType<ScenePanel>(control);
    if (scenePanel != null)
    {
      SelectScenePanel(scenePanel);
    }
  }

  public void SelectScenePanel(ScenePanel scenePanel)
  {
    DeselectScenePanel();
    _selectedScenePanel = scenePanel;
    _selectedScenePanel.Select();
    if (DataContext is SceneViewModel vm)
    {
      vm.SelectSceneById(_selectedScenePanel.SceneId);
    }
  }

  public void DeselectScenePanel()
  {
    if (_selectedScenePanel == null)
      return;
    // Return selected panel back to normal background color
    _selectedScenePanel.Deselect();
    _selectedScenePanel = null;
    if (DataContext is SceneViewModel vm)
    {
      vm.DeselectScene();
    }
  }
  private void OnDataContextChanged(object? sender, EventArgs e)
  {
    if (DataContext is SceneViewModel vm)
      vm.ShowDialog.RegisterHandler(DoShowDialogAsync);
  }

  private async Task DoShowDialogAsync(InteractionContext<YesNoViewModel, bool> context)
  {
    var dialog = new YesNoView
    {
      DataContext = context.Input
    };

    var window = this.GetVisualRoot() as Window;
    if (window != null)
    {
      var result = await dialog.ShowDialog<bool>(window);
      context.SetOutput(result);
    }
  }
}