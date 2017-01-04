using System;
using BashSoft.Contracts;

namespace BashSoft
{
    public class InputReader : IReader
    {
        private const string endCommand = "quit";
        private IInterpreter interpreter;

        public InputReader(IInterpreter interpreter)
        {
            this.interpreter = interpreter;
        }

        public void StartReadingCommands()
        {
            while (true)
            {
                OutputWriter.WriteMessage($"{SessionData.currentPath}> ");

                string input = Console.ReadLine().Trim();

                if (input == endCommand)
                {
                    break;
                }

                this.interpreter.InterpreterCommand(input);
            }
        }
        
    }
}
