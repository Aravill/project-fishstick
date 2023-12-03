namespace FishStick.Item
{
    class KeyItem : BaseItem, IKey
    {
        string IKey.UnlocksContainer
        {
            get => _unlocksContainer;
        }
        private string _unlocksContainer;

        public KeyItem(
            string Id,
            string Name,
            string Description,
            string SceneDescription,
            string Type,
            bool Hidden,
            string UnlocksContainer
        )
            : base(Id, Name, Description, SceneDescription, Type, Hidden)
        {
            _unlocksContainer = UnlocksContainer;
        }
    }
}
