namespace FishStick.Util
{
  public static class ListUtils
  {
    public static T GetRandomItem<T>(IEnumerable<T> list)
    {
      int index = new Random().Next(0, list.Count());
      return list.ElementAt(index);
    }
  }
}