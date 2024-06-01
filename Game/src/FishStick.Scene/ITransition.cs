using Scene;
using System.Runtime.CompilerServices;

namespace FishStick.Scene
{

  public interface ITransition : ISceneDescribable
  {
    string NextSceneId { get; }
    string Name { get; }
  }
}
