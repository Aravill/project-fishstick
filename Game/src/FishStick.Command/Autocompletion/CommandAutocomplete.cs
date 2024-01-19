using System;
using System.Collections.Generic;
using System.Text;

namespace FishStick.Commands.Autocompletion
{
    public struct AutocompleteResult
    {
        public enum InputStatus
        {
            Invalid,
            Valid,
            Autocompleted,
        }

        public InputStatus Status { get; }

        public string CompletionSuggestion { get; }

        public AutocompleteResult(InputStatus status, string suggestion)
        {
            Status = status;
            CompletionSuggestion = suggestion;
        }
    }
    
    public class CommandAutocomplete
    {
        private Trie CmdsTrie;

        public CommandAutocomplete()
        {
            CmdsTrie = new Trie();
        }

        public void RegisterCommand(string command_keyword)
        {
            CmdsTrie.Insert(command_keyword);
        }

        public void RegisterCommand(List<string> commands)
        {
            foreach (string command in commands)
            {
                CmdsTrie.Insert(command);
            }
        }

        public void UnregisterCommand(string command_keyword)
        {
            CmdsTrie.Delete(command_keyword);
        }

        public void UnregisterCommand(List<string> commands)
        {
            foreach (string command in commands)
            {
                CmdsTrie.Delete(command);
            }
        }

        private string GetCompletionSuggestion(Trie.Node from)
        {
            return CmdsTrie.GetShortestCompleteItem(from);
        }

        // Autocompletion by gradually checking prefixes of the input. 
        // When the end of the input is reached and it contains a suffix that is a valid command prefix, find an appropriate command completion suggestion.
        public AutocompleteResult Autocomplete(string input)
        {
            string cmdCompletion = String.Empty;
            string processedInputSubstring = input.Trim();
            
            AutocompleteResult.InputStatus inputValid = AutocompleteResult.InputStatus.Invalid;

            int sectionLen = 0;

            bool inputProcessed = false;

            // input is an empty string -> return input invalid
            if (String.IsNullOrWhiteSpace(input))
            {
                inputProcessed = true;
            }

            while (!inputProcessed)
            {
                Trie.Node lcpNode = CmdsTrie.LCPNode(processedInputSubstring);
                sectionLen = CmdsTrie.LCPLen(lcpNode);
                bool sectionComplete = lcpNode.EndOfItem;

                // section of input is not a valid command -> return input invalid
                if (sectionLen == 0)
                {
                    inputValid = AutocompleteResult.InputStatus.Invalid;
                    cmdCompletion = String.Empty;
                    break;
                }

                // end of input is reached
                if (sectionLen == processedInputSubstring.Length)
                {
                    if (sectionComplete) // the entire input is a valid command
                    {
                        inputValid = AutocompleteResult.InputStatus.Valid;
                        cmdCompletion = String.Empty;
                        break;
                    }
                    else // section is a valid command prefix -> return completion suggestion
                    {
                        inputValid = AutocompleteResult.InputStatus.Autocompleted;
                        cmdCompletion = GetCompletionSuggestion(lcpNode);
                        break;
                    }
                }

                // separate valid section from the rest of the input, check validity and repeat
                processedInputSubstring = processedInputSubstring.Substring(sectionLen);

                // Check first char of substring: if there is a non-whitespace char -> input is invalid
                Char subStart = processedInputSubstring[0];
                if (!Char.IsWhiteSpace(subStart) || !sectionComplete) // also coupled in a check for if the section wasnt a complete valid command
                {
                    inputValid = AutocompleteResult.InputStatus.Invalid;
                    cmdCompletion = String.Empty;
                    break;
                }

                // Trim command separation white space and repeat
                processedInputSubstring = processedInputSubstring.TrimStart();
            }

            AutocompleteResult result = new AutocompleteResult(inputValid, cmdCompletion);
            return result;
        }


    }
}
