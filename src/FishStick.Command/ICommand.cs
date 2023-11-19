namespace FishStick.Commands
{
  public interface ICommand
  {
    static string Name { get; } = "command";
    public void Execute(string[] args);
  }
}