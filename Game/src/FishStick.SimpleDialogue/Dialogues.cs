namespace FishStick.SimpleDialogues
{
  public struct Line
  {
    public string Text;
  }

  public struct Reply
  {
    public string Text;
    public int NextLineIndex;
  }

  public class DialogueStorage()
  {
    public static void Init()
    {
      MaidenDialogues.Init();
    }
  }
}