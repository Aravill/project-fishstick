using System.Runtime.Serialization;
using FishStick.Player;
using FishStick.World;

namespace Dialogue
{
  public interface IDialogueCondition
  {
    public bool Check(PlayerController player, WorldController world);
  }

  [DataContract]
  public class HasItemCondition : IDialogueCondition
  {
    [DataMember]
    private string _itemNameOrId;

    public HasItemCondition(string itemNameOrId)
    {
      _itemNameOrId = itemNameOrId;
    }

    public bool Check(PlayerController player, WorldController world)
    {
      bool found =
        player.GetInventoryItem(_itemNameOrId) != null
        || player.GetInventoryItemByName(_itemNameOrId) != null;
      return found;
    }
  }
}
