using FishStick.Scene;

namespace Scene
{
  public interface IElement : ISceneDescribable, IHiddeable
  {
    string Id { get; }
  }
}
