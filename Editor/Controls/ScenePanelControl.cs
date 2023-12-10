using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using AvaloniaEditor.Helpers;
using AvaloniaEditor.Models;
using Material.Styles.Controls;
namespace AvaloniaEditor.Controls
{
  public class ScenePanel : Panel
  {
    private bool _isPressed;
    private Point _startPoint;

    private Card? _card = null;

    private IBrush? _originalBackground = null;
    private readonly IBrush _selectedBackground = ColorUtils.GetBrushFromColor(Material.Colors.Recommended.AmberSwatch.Amber700);
    public static readonly AvaloniaProperty<string> SceneIdProperty =
         AvaloniaProperty.Register<ScenePanel, string>(nameof(SceneId));

    public string SceneId
    {
      get => GetValue(SceneIdProperty) as string ?? string.Empty;
      set => SetValue(SceneIdProperty, value);
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

    public void Select()
    {
      Card? card = GetCard();
      if (card != null)
      {
        _originalBackground = card.Background;
        card.Background = _selectedBackground;
      }

    }

    public void Deselect()
    {
      Card? card = GetCard();
      if (card != null)
        card.Background = _originalBackground;
    }

    private Card? GetCard()
    {
      if (_card != null)
        return _card;
      Card? card = (Card?)Children.Where(c => c is Card).FirstOrDefault();
      if (card != null)
        _card = card;
      return _card;
    }

    private void UpdatePanelPosition(PointerEventArgs e)
    {
      SceneModel? scene = DataContext as SceneModel;
      Point mousePosition = e.GetPosition(this);
      ItemsControl? control = ControlUtils.FindParentOfType<ItemsControl>(this);
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