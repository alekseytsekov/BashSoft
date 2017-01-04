using System;

namespace BashSoft.Exceptions
{
    public class KeyNotFoundExceptionn : Exception
    {
        private const string NotEnrolledInCourse = "Student must be enrolled in a course before you set his mark.";

        public KeyNotFoundExceptionn():base(NotEnrolledInCourse)
        {
            
        }

        public KeyNotFoundExceptionn(string message): base(message)
        {
            
        }
    }
}
