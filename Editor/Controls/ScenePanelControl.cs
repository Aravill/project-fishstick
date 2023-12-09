using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using AvaloniaEditor.Models;
namespace AvaloniaEditor.Controls
{
  public class ScenePanel : Panel
  {
    private bool _isPressed;
    private Point _startPoint;

    public static readonly AvaloniaProperty<string> PanelIdProperty =
         AvaloniaProperty.Register<ScenePanel, string>(nameof(PanelId));

    public string PanelId
    {
      get => GetValue(PanelIdProperty) as string ?? string.Empty;
      set => SetValue(PanelIdProperty, value);
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
      _isPressed = true;
      _startPoint = e.GetPosition(this);
      UpdatePanelPosition(e);

      base.OnPointerPressed(e);
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {

      _isPressed = false;
      UpdatePanelPosition(e);

      base.OnPointerReleased(e);
    }

    protected override void OnPointerMoved(PointerEventArgs e)
    {
      if (!_isPressed)
        return;

      if (Parent == null)
        return;

      UpdatePanelPosition(e);

      base.OnPointerMoved(e);
    }

    private T? FindParentOfType<T>(Control control) where T : class
    {
      var parent = control.Parent;
      while (parent != null && !(parent is T))
      {
        parent = parent.Parent;
      }
      return parent as T;
    }

    private void UpdatePanelPosition(PointerEventArgs e)
    {
      SceneModel? scene = DataContext as SceneModel;
      Point mousePosition = e.GetPosition(this);
      ItemsControl? control = FindParentOfType<ItemsControl>(this);
      Canvas? canvas = control?.ItemsPanelRoot as Canvas;
      if (canvas != null && scene != null && Parent != null)
      {
        Point? posOnCanvas = this.TranslatePoint(mousePosition, canvas);
        if (posOnCanvas.HasValue)
        {
          var offsetX = posOnCanvas.Value.X - _startPoint.X;
          var offsetY = posOnCanvas.Value.Y - _startPoint.Y;
          Canvas.SetLeft(Parent, offsetX);
          Canvas.SetTop(Parent, offsetY);
          scene.Position = new Point(offsetX, offsetY);
        }
      }
    }
  }
}