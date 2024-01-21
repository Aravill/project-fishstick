using System.Net;
using Dialogue;
using FishStick.Scripts;

namespace FishStick
{
  public static class Global
  {
    public static Dictionary<string, LiberalList<LiberalList<(string, int?, bool?)>>> Dialogues { get; } = new();

    public static Dictionary<string, DialogueData> DialogueData { get; } = new();

  }

  public class LiberalList<T> : List<T>
  {
    public new T this[int index]
    {
      get
      {
        return base[index];
      }
      set
      {
        if (Count <= index)
        {
          for (int i = Count; i < index; i++)
          {
            Add(default);
          }
          Add(value);
        }
        else
        {
          base[index] = value;
        }
      }
    }
  }
}