using Dialogue;

namespace FishStick.Render
{
  class DialogueStrategy
  {

    public static void HandleDialogue(IDialogue dialogue)
    {
      bool ongoingDialogue = true;
      bool readNextLine = true;
      while (ongoingDialogue)
      {
        IDialogueLine currentLine = dialogue.CurrentLine;
        if (readNextLine)
        {
          ConsoleController.WriteText(currentLine.Text);
          // Insert a small deylay to make the game feel more "alive" and less copy-pasty
          Thread.Sleep(300);
        }
        readNextLine = true;
        if (currentLine.IsDialogueExit)
        {
          // We're done with the dialogue, this was the NPC's last line.
          // We always end with an NPC finishing line
          ongoingDialogue = false;
          continue;
        }
        if (currentLine.Replies != null)
        {
          int selectedReply = 0;
          bool chosen = false;
          List<IReply> replies = currentLine.Replies;
          ListReplies(currentLine.Replies, selectedReply);
          int totalLines = replies.Count;
          do
          {
            {
              ConsoleKeyInfo readKeyResult = Console.ReadKey(true);
              switch (readKeyResult.Key)
              {
                case ConsoleKey.UpArrow:
                  ClearReplies(replies.Count);
                  selectedReply = selectedReply == 0 ? replies.Count - 1 : selectedReply - 1;
                  ListReplies(currentLine.Replies, selectedReply);
                  break;
                case ConsoleKey.DownArrow:
                  ClearReplies(replies.Count);
                  selectedReply = selectedReply == totalLines - 1 ? 0 : selectedReply + 1;
                  ListReplies(currentLine.Replies, selectedReply);
                  break;
                case ConsoleKey.Enter:
                  ClearReplies(replies.Count);
                  ConsoleWriter.Write(replies[selectedReply].Text)
                    .ToConsole();
                  Console.WriteLine();
                  dialogue.ContinueDialogue(replies[selectedReply]);
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
    }
    private static void ListReplies(List<IReply> replies, int selectedLine)
    {
      // Create an empty line to make the replies more readable, we will remove it later.
      Console.WriteLine();
      for (int i = 0; i < replies.Count; i++)
      {
        if (i == selectedLine)
        {
          ConsoleWriter.Write($"> {replies[i].Text}")
            .WithColor(ConsoleColor.Yellow)
            .ToConsole();
          Console.WriteLine();
          continue;
        }
        if (replies[i].WasUsed)
        {
          ConsoleWriter.Write($"{replies[i].Text}")
            .WithColor(ConsoleColor.DarkMagenta)
            .ToConsole();
          Console.WriteLine();
          continue;
        }
        ConsoleWriter.Write($"{replies[i].Text}")
          .ToConsole();
        Console.WriteLine();
      }
    }

    private static void ClearReplies(int lineCount)
    {
      int originalLine = Console.CursorTop;
      // Adding 1 to line count to clear the very bottom line and very top empty line
      for (int i = 0; i < lineCount + 2; i++)
      {
        Console.SetCursorPosition(0, originalLine - i);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, originalLine - i);
      }
    }
  }
}