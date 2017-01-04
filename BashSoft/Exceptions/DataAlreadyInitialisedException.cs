using System;

namespace BashSoft.Exceptions
{
    public class DataAlreadyInitialisedException: Exception
    {
        private const string DataAlreadyInitialisedExceptionMessage = "Data already initialized!";

        public DataAlreadyInitialisedException(): base(DataAlreadyInitialisedExceptionMessage)
        {
            
        }

        public DataAlreadyInitialisedException(string message):base(message)
        {
            
        }
    }
}
