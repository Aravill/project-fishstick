using System.Runtime.Serialization;
using FishStick.Item;
using FishStick.Player;
using FishStick.Scene;
using FishStick.World;

namespace FishStick.Save
{

  [DataContract]
  public class SaveData
  {
    [DataMember]
    private List<IScene> _scenes;
    [DataMember]
    private List<IItem> _playerItems;
    [DataMember]
    private int _playerHp;
    [DataMember]
    private string _playerCurrentSceneId;

    public List<IScene> Scenes
    {
      get => _scenes;
    }

    public List<IItem> PlayerItems
    {
      get => _playerItems;
    }

    public int PlayerHp
    {
      get => _playerHp;
    }

    public string PlayerCurrentSceneId
    {
      get => _playerCurrentSceneId;
    }

    public SaveData(WorldController world, PlayerController player)
    {
      _scenes = world.GetScenes().Where(scene => scene is BaseScene).ToList();
      _playerCurrentSceneId = player.GetCurrentSceneId();
      _playerItems = player.GetInventory();
      _playerHp = player.GetHp();
    }
  }
}