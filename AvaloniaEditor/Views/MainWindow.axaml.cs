using System;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace AvaloniaEditor.Views;

public partial class MainWindow : Window
{
  public MainWindow()
  {
    InitializeComponent();
  }

  public void AddSceneHandler(object sender, RoutedEventArgs args)
  {
    // Create a new box
    var box = new Rectangle();

    // Set the properties of the box
    box.Width = 50;
    box.Height = 50;
    box.Fill = Brushes.Red;
    box.Name = "Box";

    // Add the box to the canvas
    SceneCanvas.Children.Add(box);
  }
}