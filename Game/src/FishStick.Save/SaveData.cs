using System.Runtime.Serialization;
using FishStick.Player;
using FishStick.Scene;
using FishStick.World;

namespace FishStick.Save
{

  [DataContract]
  public class SaveData
  {
    [DataMember]
    private List<BaseScene> _scenes;
    [DataMember]
    private PlayerController _player;

    public List<BaseScene> Scenes
    {
      get => _scenes;
    }

    public PlayerController Player
    {
      get => _player;
    }

    public SaveData(WorldController world, PlayerController player)
    {
      List<BaseScene> toSave = world.GetScenes().OfType<BaseScene>().ToList();
      _scenes = toSave;
      _player = player;
    }
  }
}