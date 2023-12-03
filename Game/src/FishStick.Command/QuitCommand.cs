using FishStick.Player;
using FishStick.Render;
using FishStick.World;

namespace FishStick.Commands
{
    class QuitCommand(PlayerController player, WorldController world) : ICommand
    {
        private PlayerController _player = player;
        private WorldController _world = world;
        public static string Name = "quit";

        void ICommand.Execute(string[] args)
        {
            ConsoleController.WriteText("Thanks for playing!");
            Environment.Exit(0);
        }
    }
}
