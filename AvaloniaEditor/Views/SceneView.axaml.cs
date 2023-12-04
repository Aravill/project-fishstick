using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using MovableControl.Controls;

namespace AvaloniaEditor.Views;

public partial class SceneView : UserControl
{

  // private Panel? _selectedScenePanel;
  public SceneView()
  {
    InitializeComponent();

  }

  public void AddSceneHandler(object sender, RoutedEventArgs args)
  {
    // Create a new box


    // Add the box to the canvas
    SceneCanvas.Children.Add(CreateScenePanel());
  }

  private ScenePanel CreateScenePanel()
  {
    var panel = new ScenePanel();
    var stackPanel = new StackPanel();
    // Set the properties of the box
    panel.Width = 300;
    panel.Height = 400;
    panel.Background = Brushes.DimGray;

    stackPanel.Margin = new Thickness(20);

    Border panelBorder = new Border();
    panelBorder.BorderBrush = Brushes.Black;
    panelBorder.BorderThickness = new Thickness(2);
    panelBorder.Padding = new Thickness(2);

    TextBlock descriptionLabel = new TextBlock();
    descriptionLabel.Margin = new Thickness(0, 5, 0, 0);
    descriptionLabel.Text = "Scene Description:";
    TextBox descriptionField = new TextBox();
    descriptionField.Margin = new Thickness(0, 15, 0, 5);
    descriptionField.Watermark = "Description";
    descriptionField.Height = 100;
    descriptionField.AcceptsReturn = true;
    descriptionField.TextWrapping = TextWrapping.Wrap;

    panel.Children.Add(panelBorder);
    stackPanel.Children.Add(descriptionLabel);
    stackPanel.Children.Add(descriptionField);
    panel.Children.Add(stackPanel);

    return panel;
  }
}