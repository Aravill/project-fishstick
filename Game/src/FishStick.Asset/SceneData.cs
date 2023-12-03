namespace FishStick.AssetData
{
    public class SceneData(string id, string description, string name)
    {
        public string Id { get; set; } = id;
        public string Description { get; set; } = description;
        public string Name { get; set; } = name;
    }
}
