using System.Collections.Generic;

namespace BashSoft.Contracts
{
    public interface IDataSorter
    {
        void PrintSortedStudents(Dictionary<string, double> studentMarks, string comparison, int studentsToTake);
    }
}
