using System;

namespace BashSoft.Exceptions
{
    public class InvalidFileNameException : Exception
    {
        private const string ForbiddenSymbolsIsContainedInName =
            "The given names contains symbols that are not allowed to be used in names of files and folders";

        public InvalidFileNameException():base(ForbiddenSymbolsIsContainedInName)
        {
                
        }
        public InvalidFileNameException(string message):base(message)
        {
                
        }
    }
}
