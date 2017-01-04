using System;
using System.Collections.Generic;
using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft
{
    public class RepositoryFilter : IDataFilter
    {
        public void PrintFilteredStudents(Dictionary<string, double> studentsWithMarks, string wantedFilter,
            int studentsToTake)
        {


            if (wantedFilter == "excellent")
            {
                this.FilterAndTake(studentsWithMarks, x => x >= 5, studentsToTake);
            }
            else if (wantedFilter == "average")
            {
                this.FilterAndTake(studentsWithMarks, x => x < 5 && x >= 3.5, studentsToTake);
            }
            else if (wantedFilter == "poor")
            {
                this.FilterAndTake(studentsWithMarks, x => x <3.5, studentsToTake);
            }
            else
            {
                throw new InvalidFilterException();
                //throw new ArgumentException(ExceptionMessages.InvalidStudentsFilter);
                //OutputWriter.DisplayException(ExceptionMessages.InvalidStudentsFilter);
            }
        }

        private void FilterAndTake(Dictionary<string, double> studentsWithMarks, Predicate<double> givenFilter,
            int studentsToTake)
        {
            int counterForPrinted = 0;

            foreach (var studentMark in studentsWithMarks)
            {
                if (counterForPrinted == studentsToTake)
                {
                    break;
                }
                
                if (givenFilter(studentMark.Value))
                {
                    OutputWriter.PrintStudent(studentMark);
                    OutputWriter.PrintStudent(new KeyValuePair<string, double>(studentMark.Key, studentMark.Value));
                    counterForPrinted++;
                }
            }


        }

        //private static double Average(List<int> scoresOnTasks)
        //{
        //    int totalScore = 0;

        //    foreach (var scoresOnTask in scoresOnTasks)
        //    {
        //        totalScore += scoresOnTask;
        //    }

        //    double percentageOfAll = totalScore / scoresOnTasks.Count;

        //    double mark = (percentageOfAll / 100) * 4 + 2;

        //    return mark;
        //}

        //private static bool ExcelentFilter(double mark)
        //{
        //    return mark >= 5.0;
        //}

        //private static bool AverageFilter(double mark)
        //{
        //    return mark >= 3.5 && mark < 5.0;
        //}

        //private static bool PoorFilter(double mark)
        //{
        //    return mark < 3.5;
        //}
    }
}
