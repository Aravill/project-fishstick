using FishStick.Player;
using FishStick.World;

namespace FishStick.Scripts
{
  // TODO: Unify this with IScript interface. It should be easilly doable, the IScript interface
  // should basically be just this, but because i wanted to serialize it in CSV, it works slightly
  // differently
  interface IDialogueScript
  {
    public void Execute(PlayerController player, WorldController world);
  }
}
