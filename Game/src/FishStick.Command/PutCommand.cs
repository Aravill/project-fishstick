using FishStick.Item;
using FishStick.Player;
using FishStick.Render;
using FishStick.Scene;
using FishStick.World;

namespace FishStick.Commands
{
    class PutCommand(PlayerController player, WorldController world) : ICommand
    {
        private PlayerController _player = player;
        private WorldController _world = world;
        public static string Name = "put";

        /// <summary>
        /// Put an item from inventory into a designated inventory or scene container
        /// </summary>
        /// <param name="args">Args which make up the item's name and container name</param>
        void ICommand.Execute(string[] args)
        {
            // first argument will be the what we're unlocking, then the word "with" and then the key
            // "(unlock) door with metal key"
            int into = Array.IndexOf(args, "into");
            if (into == -1)
            {
                ConsoleController.WriteText("What you want to put into what?");
                return;
            }
            string itemName = String.Join(" ", args[0..into]);
            string containerName = String.Join(" ", args[(into + 1)..]);
            // Make sure the player has the item in their inventory
            IItem? item = _player.GetInventoryItem(itemName);
            if (item == null)
            {
                ConsoleController.WriteText($"You don't have a {itemName} on you.");
                return;
            }
            // Find container in inventory
            IContainer? container = FindContainer(containerName);
            if (container == null || container.Hidden)
            {
                ConsoleController.WriteText($"There is no {containerName} here.");
                return;
            }
            if (container is not IContainer)
            {
                ConsoleController.WriteText($"The {containerName} cannot hold items.");
                return;
            }
            if (container.Locked)
            {
                ConsoleController.WriteText($"The {containerName} is locked.");
                return;
            }
            if (item is IContainer)
            {
                ConsoleController.WriteText($"You can't put a container into another container.");
                return;
            }
            if (item == container)
            {
                ConsoleController.WriteText($"You can't put the {containerName} into itself.");
                return;
            }
            // TODO: In the future, we could check if items in the container don't fill it up already and if the container has space for more items. Right now, it's assumed that the container has infinite space.
            // Add item into the container and remove it from the inventory
            container.Contents.Add(item);
            _player.RemoveItem(item);
            ConsoleController.WriteText($"You put the {itemName} into the {containerName}.");
        }

        /// <summary>
        /// Find a container in the player's inventory or in the scene
        /// </summary>
        /// <param name="containerName">Name of the container we're searching for</param>
        /// <returns>The found container or null</returns>
        private IContainer? FindContainer(string containerName)
        {
            // Find container in inventory
            IContainer? container = _player.GetInventoryItem(containerName) as IContainer;
            if (container == null)
            {
                // Find container in scene
                IScene scene = _world.GetScene(_player.GetCurrentSceneId());
                container = scene.Items.Find(item => item.Name == containerName) as IContainer;
                if (container == null)
                {
                    return null;
                }
            }
            return container;
        }
    }
}
