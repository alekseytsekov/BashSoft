using System.Collections.Generic;

namespace BashSoft.Contracts
{
    public interface IDataFilter
    {
        void PrintFilteredStudents(Dictionary<string, double> studentsWithMarks, string wantedFilter,
            int studentsToTake);
    }
}
