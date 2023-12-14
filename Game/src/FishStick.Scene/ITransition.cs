namespace FishStick.Scene
{
  public interface ITransition
  {
    string NextSceneId { get; }

    string Description { get; }
    string Name { get; }
  }
}
