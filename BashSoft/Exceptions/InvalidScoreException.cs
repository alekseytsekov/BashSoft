using System;

namespace BashSoft.Exceptions
{
    public class InvalidScoreException : Exception
    {
        private const string InvalidNumberOfScores =
            "The number of scores for the given course is greater than the possible.";

        public InvalidScoreException():base(InvalidNumberOfScores)
        {
            
        }

        public InvalidScoreException(string message):base(message)
        {
            
        }
    }
}
