using System.Runtime.Serialization.Json;
using FishStick.Item;
using FishStick.Player;
using FishStick.Scene;
using FishStick.World;
using NPC;
using Scene;

namespace FishStick.Save
{
  public static class SaveManager
  {
    public static void CreateSave(PlayerController player, WorldController world, string saveName)
    {
      MemoryStream stream = new MemoryStream();
      var knownTypes = new List<Type>() { typeof(BaseScene), typeof(BaseTransition), typeof(BaseItem), typeof(ContainerItem), typeof(KeyItem), typeof(InteractableElement), typeof(StaticElement), typeof(NonPlayableCharacter) };
      var ser = new DataContractJsonSerializer(typeof(Save), knownTypes);
      Save s = new Save(world, player, saveName);
      ser.WriteObject(stream, s);
      byte[] file = stream.ToArray();
      stream.Close();
      string name = GetSaveName(s);
      File.WriteAllBytes($"{name}.json", file);
    }

    private static string GetSaveName(Save s)
    {
      return $"save_{s.Name.ToLower()}_{s.Created}";
    }
  }
}