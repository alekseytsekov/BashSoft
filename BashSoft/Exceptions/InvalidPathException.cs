using System;

namespace BashSoft.Exceptions
{
    public class InvalidPathException : Exception
    {
        private const string InvalidPath = "The folder/file you are trying to access at the current adress, does not exist.";
        private const string InvalidPathWithMessage = "The '{0}' folder/file you are trying to access at the current adress, does not exist.";

        public InvalidPathException():base(InvalidPath)
        {
             
        }

        public InvalidPathException(string message):base(string.Format(InvalidPathWithMessage, message))
        {

        }
    }
}
