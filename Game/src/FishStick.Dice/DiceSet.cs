namespace FishStick.Dice
{
  record DiceSet(int Sides, int Count = 1)
  {
    public DiceSet(string diceNotation) : this(Parse(diceNotation)) { }

    public static implicit operator DiceSet(string diceNotation) => Parse(diceNotation);

    private static DiceSet Parse(string diceNotation)
    {
      string[] parts = diceNotation.Split('d');

      return parts.Length switch
      {
        1 when int.TryParse(parts[0], out int sides)
            => new DiceSet(sides),

        2 when int.TryParse(parts[0], out int diceCount) && int.TryParse(parts[1], out int sides)
            => new DiceSet(sides, diceCount),

        _ => throw new ArgumentException("Invalid dice notation format.")
      };
    }

    public int Roll() => 
      Enumerable.Range(0, Count)
                .Select(_ => Roll(Sides))
                .Sum();

    private static int Roll(int sides) => Random.Shared.Next(1, sides + 1);
  }
}
