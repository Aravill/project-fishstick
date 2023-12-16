using System.Runtime.Serialization;

namespace Scene
{
  [DataContract]
  public class StaticElement : IElement
  {
    public StaticElement(string id, string sceneDescription, bool hidden)
    {
      Id = id;
      SceneDescription = sceneDescription;
      Hidden = hidden;
    }
    [DataMember]
    public string Id { get; }
    [DataMember]
    public string SceneDescription { get; }
    [DataMember]
    public bool Hidden { get; set; }
  }
}
