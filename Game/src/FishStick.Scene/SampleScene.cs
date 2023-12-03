using Dialogue;
using FishStick.Item;
using NPC;
using Scene;

namespace FishStick.Scene
{
  public static class SampleScene
  {
    public static BaseScene BuildSampleScenes()
    {
      return new BaseScene(
        id: "scene-0",
        description: "You are in a big stone room.",
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
            "There is a small metal key on the floor.",
            "key",
            false
          )
        },
        elements: new List<IElement>(),
        npcs: new List<INonPlayableCharacter>()
        {
          new NonPlayableCharacter(
            "npc-1",
            "Maiden",
            "A young {Maiden} stands in the corner of the room.",
            SampleDialogue.GetSampleDialogues()
          )
        }
      );
    }
  }
}
