using Dialogue;

namespace FishStick.Render
{
  class DialogueStrategy
  {
    public static List<IReply> HandleDialogue(IDialogue dialogue)
    {
      bool ongoingDialogue = true;
      bool readNextLine = true;
      List<IReply> chosenReplies = new List<IReply>();
      while (ongoingDialogue)
      {
        IDialogueLine currentLine = dialogue.CurrentLine;
        if (readNextLine)
        {
          ConsoleController.WriteText(currentLine.Text);
          // Insert a small deylay to make the game feel more "alive" and less copy-pasty
          Thread.Sleep(100);
        }
        readNextLine = true;
        if (currentLine.IsDialogueExit)
        {
          // We're done with the dialogue, this was the NPC's last line.
          // We always end with an NPC finishing line
          ongoingDialogue = false;
          dialogue.EndDialogue();
          continue;
        }
        if (currentLine.Replies != null)
        {
          // TODO: I am unsure, but i think this could be simplified...
          // we could begin our DO cycle by listing replies and always ending it by clearing...
          int selectedReply = 0;
          bool chosen = false;
          // Filter out replies that have been used and are not repeatable
          List<IReply> usableReplies = FilterUsableReplies(currentLine);
          ListReplies(usableReplies, selectedReply);
          do
          {
            {
              usableReplies = FilterUsableReplies(currentLine);
              int totalLines = usableReplies.Count;
              ConsoleKeyInfo readKeyResult = Console.ReadKey(true);
              switch (readKeyResult.Key)
              {
                case ConsoleKey.UpArrow:
                  ClearReplies(usableReplies.Count);
                  selectedReply = selectedReply == 0 ? usableReplies.Count - 1 : selectedReply - 1;
                  ListReplies(usableReplies, selectedReply);
                  break;
                case ConsoleKey.DownArrow:
                  ClearReplies(usableReplies.Count);
                  selectedReply = selectedReply == totalLines - 1 ? 0 : selectedReply + 1;
                  ListReplies(usableReplies, selectedReply);
                  break;
                case ConsoleKey.Enter:
                  ClearReplies(usableReplies.Count);
                  IReply chosenReply = usableReplies[selectedReply];
                  ConsoleWriter.Write(chosenReply.Text).ToConsole();
                  // TODO: I currently cannot think of a better way to process replies than this
                  // This class should not handle script execution, but it is the only class
                  // that knows about the selected replies
                  if (!chosenReplies.Contains(chosenReply))
                  {
                    chosenReplies.Add(chosenReply);
                  }
                  Console.WriteLine();
                  dialogue.ContinueDialogue(chosenReply);
                  chosen = true;
                  break;
                default:
                  break;
              }
            }
          } while (!chosen);
        }
        else if (currentLine.NextLineId != null)
        {
          // The line has no replies, but it does have a next line, so we continue the dialogue
          dialogue.ContinueDialogue(currentLine);
          readNextLine = currentLine.ReadNextLine ?? true;
        }
        else
        {
          // This is a line with no replies and no next line, so we're done with the dialogue
          // This should cover dead ends
          ongoingDialogue = false;
        }
      }
      return chosenReplies;
    }

    private static List<IReply> FilterUsableReplies(IDialogueLine currentLine)
    {
      return currentLine
          .Replies
          ?.Where(reply => reply.WasUsed == false || reply.Repeatable == true)
          .ToList() ?? new List<IReply>();
    }

    private static void ListReplies(List<IReply> replies, int selectedLine)
    {
      // Create an empty line to make the replies more readable, we will remove it later.
      // Console.WriteLine();
      for (int i = 0; i < replies.Count; i++)
      {
        if (i == selectedLine)
        {
          ConsoleWriter.Write($"> {replies[i].Text}").WithColor(ConsoleColor.Yellow).ToConsole();
          Console.WriteLine();
          continue;
        }
        if (replies[i].WasUsed)
        {
          ConsoleWriter.Write($"{replies[i].Text}").WithColor(ConsoleColor.DarkMagenta).ToConsole();
          Console.WriteLine();
          continue;
        }
        ConsoleWriter.Write($"{replies[i].Text}").ToConsole();
        Console.WriteLine();
      }
    }

    private static void ClearReplies(int lineCount)
    {
      int originalLine = Console.CursorTop;
      // Adding 1 to line count to clear the very bottom line and very top empty line
      for (int i = 0; i < lineCount + 1; i++)
      {
        Console.SetCursorPosition(0, originalLine - i);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, originalLine - i);
      }
    }
  }
}
