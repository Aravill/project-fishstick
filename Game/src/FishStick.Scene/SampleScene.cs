using Character;
using FishStick.Item;
using Scene;

namespace FishStick.Scene
{
  public static class SampleScene
  {
    public static BaseScene BuildSampleScenes()
    {
      return new BaseScene(
        id: "scene-0",
        description: "You are in a snowy forest clearing.",
        // exits: new List<ITransition>() {
        //   new BaseTransition(
        //     name: "north",
        //     description: "A door leads {north}.",
        //     nextSceneId: "scene-2"
        //   )
        // },
        exits: new List<ITransition>(),
        items: new List<IItem>()
        {
          new BaseItem(
            "key-1",
            "metal key",
            "A small metal key.",
            "There is a {metal key} on the floor.",
            "key",
            false
          )
        },
        elements: new List<IElement>(),
        npcs: new List<NPC>()
        {
          new NPC(
            "npc-1",
            "Maiden",
            "A young {Maiden} stands near a small campfire nearby.",
            new List<string>()
            {
              "maiden.first-meeting",
              "maiden.generic",
              "maiden.key-1-found"
            }
          )
        }
      );
    }
  }
}
