using Scene;

namespace Character
{
  public class NPC : ISceneDescribable
  {
    public string Id { get; }

    public string Name { get; }

    public string SceneDescription { get; }

    public List<string> Dialogues { get; }

    public NPC(
      string id,
      string name,
      string sceneDescription,
      List<string> dialogues
    )
    {
      Id = id;
      Name = name;
      SceneDescription = sceneDescription;
      Dialogues = dialogues;
    }
  }
}
