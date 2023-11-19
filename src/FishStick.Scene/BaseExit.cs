
namespace FishStick.Scene
{
  public class BaseTransition(string name, string description, string nextRoomId) : ITransition
  {

    public string Name { get; } = name;
    public string Description { get; } = description;
    public string NextSceneId { get; } = nextRoomId;
    public bool Highlight => true; // TODO: Add to the CSV
  }
}
