namespace Scene
{
  public class StaticElement : IElement
  {
    public StaticElement(string id, string sceneDescription, bool hidden)
    {
      Id = id;
      SceneDescription = sceneDescription;
      Hidden = hidden;
    }

    public string Id { get; }

    public string SceneDescription { get; }

    public bool Hidden { get; set; }
  }
}
