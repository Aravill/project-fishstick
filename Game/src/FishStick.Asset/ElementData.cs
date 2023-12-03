namespace FishStick.AssetData
{
    public class ElementData(
        string id,
        string sceneDescription,
        bool hidden,
        string type,
        string inScene
    )
    {
        public string Id { get; set; } = id;
        public string InScene { get; set; } = inScene;

        public string SceneDescription { get; set; } = sceneDescription;

        public bool Hidden { get; set; } = hidden;

        public string Type { get; set; } = type;
    }
}
