namespace FishStick.Dice
{
  internal static class DiceRoller
  {
    public static int Roll(string diceNotation)
    {
      DiceSet diceSet = diceNotation;
      return diceSet.Roll();
    }
  }
}
