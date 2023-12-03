using FishStick.Player;
using FishStick.World;

namespace Dialogue
{
  public interface IDialogueCondition
  {
    public bool Check(PlayerController player, WorldController world);
  }
}
