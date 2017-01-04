

using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace BashSoft
{
    public static class ExceptionMessages
    {
        public const string ExampleExceptionMessage = "Example Message!";

        //public const string DataAlreadyInitialisedException = "Data already initialized!";

        public const string DataNotInitializedExceptionMessage = "The data structure must be initialised first in order to make any operation with it.";

        public const string InexistingCourseInDataBase = "The course you are trying to get does not exist in the data base!";

        //public const string InvalidPath = "The folder/file you are trying to access at the current adress, does not exist.";

        public const string UnauthorizedAccessEXceptionMessage = "The folder/file you are trying to get access needs a higher level of rights than you currently hava.";

        public const string ComprasionOfFilesWithDiffrentSizes = "Files not equal size, certain mismatch.";

        //public const string ForbiddenSymbolsIsContainedInName = "The given names contains symbols that are not allowed to be used in names of files and folders";

        public const string UnableToToGoHigherInPartitionHierarchy = "Length cannot be less then zero.";

        public const string UnableToParseNumber = "The sequence you've written is not a valid number.";

        //public const string InvalidStudentsFilter = "The given filter is not one of the following: excelent/average/poor!";

        //public const string InvalidComparisonQuery = "The comparison query you want, does not exist in the context of the current program!";

        public const string InvalidTakeQuantityParameter = "The take command expected does not match the format wanted!";

        //public const string StudentAlreadyEnrolledInGivenCourse = "The {0} already exists in {1}";

        //public const string NotEnrolledInCourse = "Student must be enrolled in a course before you set his mark.";

        public const string InvalidNumberOfScores =
            "The number of scores for the given course is greater than the possible.";

        public const string InvalidScore = "The number for the score you've entered is not in the range 0-100.";

        //public const string NullOrEmptyValue = "The value of the variable CANNOT be null or empty!";

        //public const string InvalidDestination = "You are unable to go higher in the hierarchy of the current partition.";
    }
}
