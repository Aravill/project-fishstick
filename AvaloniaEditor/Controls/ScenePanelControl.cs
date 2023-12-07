using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Input;
using Avalonia.Media;
using AvaloniaEditor.Models;
namespace AvaloniaEditor.Controls
{
  public class ScenePanel : Panel
  {
    private bool _isPressed;
    private Point _positionInBlock;
    private TranslateTransform _transform = null!;


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
      _positionInBlock = e.GetPosition(this);

      if (_transform != null!)
        _positionInBlock = new Point(
            _positionInBlock.X - _transform.X,
            _positionInBlock.Y - _transform.Y);

      base.OnPointerPressed(e);
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {

      _isPressed = false;
      var viewModel = DataContext as Scene;
      if (viewModel != null)
      {
        var position = e.GetPosition(this);
        viewModel.Position = new Point(position.X, position.Y);
      }

      base.OnPointerReleased(e);
    }

    protected override void OnPointerMoved(PointerEventArgs e)
    {
      if (!_isPressed)
        return;

      if (Parent == null)
        return;

      var currentPosition = e.GetPosition(this);

      var offsetX = currentPosition.X - _positionInBlock.X;
      var offsetY = currentPosition.Y - _positionInBlock.Y;

      _transform = new TranslateTransform(offsetX, offsetY);
      RenderTransform = _transform;

      base.OnPointerMoved(e);
    }
  }
}