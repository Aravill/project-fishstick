using System.Runtime.Serialization;
using Avalonia;

namespace AvaloniaEditor.Models
{

  [DataContract]
  public class Scene
  {

    [IgnoreDataMember]
    public Point Position
    {
      get => new Point(_x, _y);
      set
      {
        _x = value.X;
        _y = value.Y;
      }
    }
    [DataMember]
    public string Id { get; set; } = string.Empty;
    [DataMember]
    public string Description { get; set; } = string.Empty;


    [DataMember]
    private double _x;

    [DataMember]
    private double _y;

    [IgnoreDataMember]
    public double X { get => _x; }
    [IgnoreDataMember]
    public double Y { get => _y; }
  }

}