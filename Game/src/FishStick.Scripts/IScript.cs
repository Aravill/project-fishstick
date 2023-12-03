using FishStick.Player;
using FishStick.World;

namespace FishStick.Scripts
{
    interface IScript
    {
        public void Execute(PlayerController player, WorldController world, string[] args);
    }
}
