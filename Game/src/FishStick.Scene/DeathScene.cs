using FishStick.Item;
using NPC;
using Scene;

namespace FishStick.Scene
{
  public class DeathScene : BaseScene
  {

    public static string DeathSceneId = "death";

    public DeathScene() : base(DeathSceneId, "You have died. Game over. Type 'quit' to exit or 'restart' to try again.", new List<ITransition>(), new List<IItem>(), new List<IElement>(), new List<INonPlayableCharacter>())
    {
    }

  }
}