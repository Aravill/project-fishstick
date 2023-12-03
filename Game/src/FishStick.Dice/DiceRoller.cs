namespace FishStick.Dice
{
    class DiceRoller
    {
        public static int Roll(string diceNotation)
        {
            string[] diceParts = diceNotation.Split('d');
            int sides = int.Parse(diceParts[1]);
            int diceCount = int.Parse(diceParts[0]);
            int total = 0;
            for (int i = 0; i < diceCount; i++)
            {
                total += Roll(sides);
            }
            return total;
        }

        public static int Roll(int sides)
        {
            return new Random().Next(1, sides + 1);
        }
    }
}
