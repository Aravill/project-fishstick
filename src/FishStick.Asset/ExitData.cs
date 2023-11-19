
namespace FishStick.AssetData
{
  public class ExitData(string name, string from, string to, string description)
  {
    public string Name { get; } = name;
    public string From { get; } = from;

    public string To { get; } = to;

    public string Description { get; } = description;
  }
}
