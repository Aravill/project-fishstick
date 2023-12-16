using Avalonia.Media;

namespace AvaloniaEditor.Helpers
{
  public static class ColorUtils
  {
    public static IBrush GetBrushFromColor(Color color)
    {
      return new Avalonia.Media.Immutable.ImmutableSolidColorBrush(
        Color.FromArgb(color.A, color.R, color.G, color.B));
    }
  }
}