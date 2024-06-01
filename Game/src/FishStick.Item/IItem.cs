using FishStick.Scene;
using Scene;

namespace FishStick.Item
{
  public interface IItem : ISceneDescribable, IHiddeable
  {
    // Returns the item's name
    string Name { get; }

    // Returns the item's description
    string Description { get; }    

    // Returns the item's type
    string Type { get; }

    // Returns the item's id
    string Id { get; }
  }
}
