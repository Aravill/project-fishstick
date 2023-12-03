namespace FishStick.Item
{
  interface IKey : IItem
  {
    string UnlocksContainer { get; }
  }
}