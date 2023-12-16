namespace FishStick.Combat.Narration
{
  public class Range
  {
    public int Start { get; }
    public int End { get; }

    public Range(int start, int end)
    {
      Start = start;
      End = end;
    }

    public bool IsWithinRange(int value)
    {
      return value >= Start && value <= End;
    }
  }
}