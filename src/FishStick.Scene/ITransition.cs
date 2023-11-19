
namespace FishStick.Scene
{
  public interface ITransition
  {

    string NextSceneId { get; }

    string Description { get; }
    string Name { get; }
    bool Highlight { get; } // TODO: Add to csv
  }
}

