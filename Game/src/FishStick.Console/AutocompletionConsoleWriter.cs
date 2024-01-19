using FishStick.Commands.Autocompletion;
using System;
using Render;
using System.Threading.Tasks.Dataflow;

namespace FishStick.Render
{
    static class AutocompletionConsoleWriter{

        const char VALID = '✓';     
        const char INVALID = 'X';
        const ConsoleColor VALID_CLR = ConsoleColor.Green;
        const ConsoleColor INVALID_CLR = ConsoleColor.Red;
        const ConsoleColor AUTOCOMPLETE_CLR = ConsoleColor.DarkGray;

        public static void WriteResult(AutocompleteResult result)
        {
            string output = "";
            ConsoleColor outputColor = ConsoleColor.White;
            switch(result.Status)
            {
                case AutocompleteResult.InputStatus.Invalid:
                    output = "   " + INVALID;
                    outputColor = INVALID_CLR;
                    break;
                case AutocompleteResult.InputStatus.Valid:
                    output = "   " + VALID;
                    outputColor = VALID_CLR;
                    break;
                case AutocompleteResult.InputStatus.Autocompleted:
                    output = result.CompletionSuggestion;
                    outputColor = AUTOCOMPLETE_CLR;
                    break;
            }
            ConsoleColor tmpClr = Console.ForegroundColor;
            Console.ForegroundColor = outputColor;
            Console.Write(output);
            Console.ForegroundColor = tmpClr;
        }
    }
}
