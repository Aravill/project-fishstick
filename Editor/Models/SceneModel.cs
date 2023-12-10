using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Avalonia;
using FishStick.Item;
using FishStick.Scene;
using Scene;

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
    public string Name { get; set; } = "Room";

    [DataMember]
    public string Description { get; set; } = string.Empty;

    [IgnoreDataMember]
    public string ShortDescription { get => Description.Length > 42 ? Description.Substring(0, 42).Trim() + "..." : Description; }

    [DataMember]
    public List<ITransition> Transitions { get; set; } = new();

    [DataMember]
    public List<IItem> Items { get; } = new();

    [DataMember]
    public List<IElement> Elements { get; } = new();


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