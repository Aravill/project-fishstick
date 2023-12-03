namespace Scene
{
    public interface IElement
    {
        string Id { get; }
        string SceneDescription { get; }
        bool Hidden { get; set; }
    }
}
