
using System.Runtime.Serialization;

namespace FishStick.Scene
{
  [DataContract]
  public class BaseTransition(string name, string description, string nextRoomId) : ITransition
  {

    [DataMember]
    public string Name { get; set; } = name;
    [DataMember]
    public string Description { get; set; } = description;
    [DataMember]
    public string NextSceneId { get; set; } = nextRoomId;
  }
}
