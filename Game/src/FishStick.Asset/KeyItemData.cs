namespace FishStick.AssetData
{
    public class KeyItemData : ItemData
    {
        public string UnlocksContainer { get; set; }

        public KeyItemData(
            string id,
            string name,
            string description,
            string sceneDescription,
            string type,
            string inScene,
            bool Hidden,
            string UnlocksContainer
        )
            : base(id, name, description, sceneDescription, type, inScene, Hidden)
        {
            this.UnlocksContainer = UnlocksContainer;
        }
    }
}
