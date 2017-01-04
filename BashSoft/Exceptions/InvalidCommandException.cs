using System;

namespace BashSoft.Exceptions
{
    public class InvalidCommandException : Exception
    {
        private const string DisplayInvalidCommandExceptionMessage = "The command '{0}' is invalid.";

        public InvalidCommandException():base()
        {

        }

        public InvalidCommandException(string input) : base(string.Format(DisplayInvalidCommandExceptionMessage, input))
        {

        }

        //public void DisplayInvalidCommandExceptionMessage(string input)
        //{
        //    OutputWriter.DisplayException($"The command '{input}' is invalid.");
        //}
    }
}
