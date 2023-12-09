using System;
using System.Runtime.Serialization;
using Avalonia;
// using FishStick.Scene;

namespace AvaloniaEditor.Models
{

  [DataContract]
  public class SceneModel
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
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [DataMember]
    public string Description { get; set; } = string.Empty;

    // [DataMember]
    // List<ITransition> Transitions { get; }


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