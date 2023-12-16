using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using FishStick.Item;
using FishStick.Player;
using FishStick.Scene;
using FishStick.World;

namespace FishStick.Save
{
  [DataContract]
  public class Save
  {
    [DataMember]
    private string _id = Guid.NewGuid().ToString();
    [DataMember]
    private string _name = string.Empty;
    [DataMember]
    private DateTime _created = DateTime.Now;
    [DataMember]
    private SaveData _saveData;


    public string Id
    {
      get => _id;
    }

    public string Name
    {
      get => _name;
      private set => _name = value;
    }

    public DateTime Created
    {
      get => _created;
    }

    public SaveData SaveData
    {
      get => _saveData;
    }

    public Save(WorldController world, PlayerController player, string SaveName)
    {
      Name = SaveName;
      _saveData = new SaveData(world, player);
    }

    public void Persist()
    {
      MemoryStream stream = new MemoryStream();
      var ser = new DataContractJsonSerializer(typeof(IEnumerable<Save>));
      ser.WriteObject(stream, this);
      byte[] file = stream.ToArray();
      stream.Close();
      File.WriteAllBytes($"{GetSaveName()}.json", file);
    }

    private string GetSaveName()
    {
      return $"save_{Name.ToLower()}_{Created}";
    }

  }
}