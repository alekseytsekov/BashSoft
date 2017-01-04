using System.Collections.Generic;

namespace BashSoft.Contracts
{
    public interface IRequester
    {
        void GetAllStudentsFromCourse(string courseName);

        void GetStudentScoresFromCourse(string courseName, string userName);

        ISimpleOrderBag<ICourse> GetAllCoursesSorted(IComparer<ICourse> cmp);
        ISimpleOrderBag<IStudent> GetAllStudentsSorted(IComparer<IStudent> cmp);
    }
}
