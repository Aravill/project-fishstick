
using System;
using System.Drawing;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using AvaloniaEditor.Controls;
using AvaloniaEditor.Helpers;

namespace AvaloniaEditor.Views;

public partial class SceneView : UserControl
{

  private ScenePanel? _selectedScenePanel { get; set; }
  public SceneView()
  {
    InitializeComponent();
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
  }

  public void DeselectScenePanel()
  {
    if (_selectedScenePanel == null)
      return;
    // Return selected panel back to normal background color
    _selectedScenePanel.Deselect();
    _selectedScenePanel = null;
  }
}