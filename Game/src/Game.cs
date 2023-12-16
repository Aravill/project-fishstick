class Program
{
  static void Main(string[] args)
  {
    try
    {
      GameController game = new();
      game.Start();
    }
    catch (Exception exception)
    {
      Console.WriteLine(exception.Message);
      Console.WriteLine(exception.StackTrace);
      Console.CursorVisible = true;
      Environment.Exit(0);
    }
  }
}