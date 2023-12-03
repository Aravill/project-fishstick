using FishStick.Item;
using FishStick.Player;
using FishStick.Render;
using FishStick.Scene;
using FishStick.World;

namespace FishStick.Commands
{
    class UnlockCommand(PlayerController player, WorldController world) : ICommand
    {
        private PlayerController _player = player;
        private WorldController _world = world;
        public static string Name = "unlock";

        void ICommand.Execute(string[] args)
        {
            // first argument will be the what we're unlocking, then the word "with" and then the key
            // "(unlock) door with metal key"
            int withIndex = Array.IndexOf(args, "with");
            if (withIndex == -1)
            {
                ConsoleController.WriteText("What you want to unlock and with what?");
                return;
            }
            string itemName = String.Join(" ", args[0..withIndex]);
            string keyName = String.Join(" ", args[(withIndex + 1)..]);
            // Make sure the player has the key in their inventory
            IKey? key = _player.GetInventoryItem(keyName) as IKey;
            if (key == null)
            {
                ConsoleController.WriteText($"You don't have a {keyName} on you.");
                return;
            }
            // Find first in inventory
            IContainer? item = _player.GetInventoryItem(itemName) as IContainer;
            if (item == null)
            {
                // Find in scene
                IScene scene = _world.GetScene(_player.GetCurrentSceneId());
                item = scene.Items.Find(item => item.Name == itemName) as IContainer;
                if (item == null)
                {
                    // Item not found
                    ConsoleController.WriteText($"There is no {itemName} here to unlock.");
                    return;
                }
            }
            // Check that the item is actually an unlockable container
            if (item is not IContainer)
            {
                ConsoleController.WriteText($"The {itemName} cannot be unlocked.");
                return;
            }
            if (key.UnlocksContainer != itemName)
            {
                ConsoleController.WriteText($"The {keyName} doesn't seem to fit {itemName}.");
                return;
            }
            if (!item.Locked)
            {
                ConsoleController.WriteText($"The {itemName} is not locked.");
                return;
            }
            item.Unlock();
            ConsoleController.WriteText(
                $"You unlock the {itemName} with the {keyName}. It can now be opened."
            );
        }
    }
}
