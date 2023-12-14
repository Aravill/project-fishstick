namespace FishStick.Scene
{
  public class BaseTransition(string name, string description, string nextSceneId) : ITransition
  {
    public string Name { get; } = name;
    public string Description { get; } = description;
    public string NextSceneId { get; } = nextSceneId;
  }
}