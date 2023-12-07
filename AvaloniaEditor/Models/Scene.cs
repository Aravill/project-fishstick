using Avalonia;

namespace AvaloniaEditor.Models
{
  public class Scene
  {

    public Point Position { get; set; } = new Point(0, 0);
    public string Id { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public double X => Position.X;
    public double Y => Position.Y;

  }

}