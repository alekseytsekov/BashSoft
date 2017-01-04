using System;

namespace BashSoft.Exceptions
{
    public class InvalidFilterException : Exception
    {
        private const string InvalidStudentsFilter = "The given filter is not one of the following: excelent/average/poor!";

        public InvalidFilterException(): base(InvalidStudentsFilter)
        {
            
        }

        public InvalidFilterException(string message):base(message)
        {
            
        }
    }
}
