using FishStick.Session;

namespace FishStick.Render
{
    public class GameCursor
    {
        public char cursorSymbol = '▮';
        public int cursorIndex = 0;
    }

    static class CommandStrategy
    {
        public static string ReadCommand(SessionHistory history)
        {
            Console.CursorVisible = false;
            string? finalInput = null;
            GameCursor cursor = new();
            // Fake the cursor
            string writtenInput = cursor.cursorSymbol.ToString();
            Console.Write(writtenInput);
            do
            {
                ConsoleKeyInfo readKeyResult = Console.ReadKey(true);
                switch (readKeyResult.Key)
                {
                    case ConsoleKey.UpArrow:
                        ClearCurrentConsoleLine();
                        writtenInput = history.GetPrevious() + cursor.cursorSymbol;
                        Console.Write(writtenInput);
                        cursor.cursorIndex = writtenInput.Length - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        ClearCurrentConsoleLine();
                        writtenInput = history.GetNext() + cursor.cursorSymbol;
                        Console.Write(writtenInput);
                        cursor.cursorIndex = writtenInput.Length - 1;
                        break;
                    case ConsoleKey.Backspace:
                        if (cursor.cursorIndex > 0)
                        {
                            ClearCurrentConsoleLine();
                            // Remove the cursor symbol + last character from the end of the string
                            if (cursor.cursorIndex == 0)
                            {
                                break;
                            }
                            // Remove the symbol before the cursor
                            writtenInput = writtenInput.Remove(cursor.cursorIndex - 1, 1);
                            cursor.cursorIndex--;
                            Console.Write(writtenInput);
                        }
                        break;
                    case ConsoleKey.Enter:
                        // Don't do anything if the user hasn't typed anything yet
                        if (writtenInput.Length > 1)
                        {
                            ClearCurrentConsoleLine();
                            finalInput = RemoveCursor(writtenInput, cursor.cursorIndex);
                            Console.Write(finalInput);
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        ClearCurrentConsoleLine();
                        writtenInput = MoveCursor(false, writtenInput, cursor);
                        Console.Write(writtenInput);
                        break;
                    case ConsoleKey.RightArrow:
                        ClearCurrentConsoleLine();
                        writtenInput = MoveCursor(true, writtenInput, cursor);
                        Console.Write(writtenInput);
                        break;
                    default:
                        // Clear what's been written so far
                        ClearCurrentConsoleLine();
                        // Add the new character + cursor symbol and add it to the written input
                        string added = readKeyResult.KeyChar.ToString();
                        writtenInput = writtenInput.Insert(cursor.cursorIndex, added);
                        cursor.cursorIndex++;
                        Console.Write(writtenInput);
                        break;
                }
            } while (finalInput == null || finalInput.Length < 1);
            Console.WriteLine();
            history.Add(finalInput);
            Console.CursorVisible = true;
            return finalInput;
        }

        private static string MoveCursor(bool forward, string text, GameCursor cursor)
        {
            // Make sure we don't go out of bounds
            if (cursor.cursorIndex == 0 && !forward)
                return text;
            // -2 because our cursor is at the end of the string, always addind 1 additional symbol and the user cannot move past itself
            if (cursor.cursorIndex == text.Length - 1 && forward)
                return text;
            // Remove cursor from its current index
            text = text.Remove(cursor.cursorIndex, 1);
            // Shift the cursor index
            cursor.cursorIndex = forward ? cursor.cursorIndex + 1 : cursor.cursorIndex - 1;
            // Assemble the string with the cursor symbol
            text = text.Insert(cursor.cursorIndex, cursor.cursorSymbol.ToString());
            return text;
        }

        private static string RemoveCursor(string input, int cursorIndex)
        {
            return input.Remove(cursorIndex, 1);
        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
