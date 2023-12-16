namespace FishStick.AssetData
{
  public class ContainerItemData : ItemData
  {
    public bool Locked { get; set; }

    public ContainerItemData(
      string id,
      string name,
      string description,
      string sceneDescription,
      string type,
      string inScene,
      bool Hidden,
      bool Locked
    )
      : base(id, name, description, sceneDescription, type, inScene, Hidden)
    {
      this.Locked = Locked;
    }
  }
}
