using System.Collections.Generic;
using System.Linq;
using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft
{
    public class RepositorySorter : IDataSorter
    {
        public void PrintSortedStudents(Dictionary<string,double> studentMarks, string comparison, int studentsToTake)
        {
            comparison = comparison.ToLower();

            if (comparison == "ascending")
            {
                this.PrintStudents(studentMarks.OrderBy(x=> x.Value)
                    .Take(studentsToTake)
                    .ToDictionary(pair=> pair.Key, pair=> pair.Value) );
                
                //OrderAndTake(wantedData, studentsToTake, CompareInOrder);
            }
            else if (comparison == "descending")
            {
                this.PrintStudents(studentMarks.OrderByDescending(x => x.Value)
                    .Take(studentsToTake)
                    .ToDictionary(pair => pair.Key, pair => pair.Value));
                
                //OrderAndTake(wantedData, studentsToTake, CompareDescendingOrder);
            }
            else
            {
                throw new InvalidComparisonQueryException();
                //throw new ArgumentException(ExceptionMessages.InvalidComparisonQuery);
                //OutputWriter.DisplayException(ExceptionMessages.InvalidComparisonQuery);
            }
        }

        private void PrintStudents(Dictionary<string, double> studentsSorted)
        {

            foreach (KeyValuePair<string, double> keyValuePair in studentsSorted)
            {
                OutputWriter.PrintStudent(keyValuePair);
            }
        }
        
        //private static void OrderAndTake(Dictionary<string, List<int>> wantedData, int studentsToTake,
        //    Func<KeyValuePair<string, List<int>>, KeyValuePair<string, List<int>>, int> comparisonFunc)
        //{
            
        //    ///--------------------------------//////////////////
        //    /// 

        //    Dictionary<string, List<int>> studentsSorted = GetSortedStudents(wantedData, studentsToTake, comparisonFunc);

        //    foreach (var student in studentsSorted)
        //    {
        //        OutputWriter.PrintStudent(student);
        //    }

        //}

        //private static int CompareInOrder(KeyValuePair<string, List<int>> firstValue,
        //    KeyValuePair<string, List<int>> secondValue)
        //{
        //    int totalOfFirstMarks = 0;

        //    foreach (var valuee in firstValue.Value)
        //    {
        //        totalOfFirstMarks += valuee;
        //    }

        //    int totalOfSecondMarks = 0;

        //    foreach (var valuee in secondValue.Value)
        //    {
        //        totalOfSecondMarks += valuee;
        //    }

        //    return totalOfSecondMarks.CompareTo(totalOfFirstMarks);
        //}

        //private static int CompareDescendingOrder(KeyValuePair<string, List<int>> firstValue,
        //    KeyValuePair<string, List<int>> secondValue)
        //{
        //    int totalOfFirstMarks = 0;

        //    foreach (var valuee in firstValue.Value)
        //    {
        //        totalOfFirstMarks += valuee;
        //    }

        //    int totalOfSecondMarks = 0;

        //    foreach (var valuee in secondValue.Value)
        //    {
        //        totalOfSecondMarks += valuee;
        //    }

        //    return totalOfFirstMarks.CompareTo(totalOfSecondMarks);
        //}

        //private static Dictionary<string, List<int>> GetSortedStudents(Dictionary<string, List<int>> studentsWanted,
        //    int takeCount, Func<KeyValuePair<string, List<int>>, KeyValuePair<string, List<int>>, int> Comparison)
        //{
        //    int valuesTaken = 0;


        //    Dictionary<string, List<int>> studentsSorted = new Dictionary<string, List<int>>();

        //    KeyValuePair<string, List<int>> nextInOrder = new KeyValuePair<string, List<int>>();

        //    bool isSorted = false;

        //    while (true)
        //    {
        //        isSorted = true;

        //        foreach (var studentWithScore in studentsWanted)
        //        {
        //            if (!string.IsNullOrEmpty(nextInOrder.Key))
        //            {
        //                int comparisonResult = Comparison(studentWithScore, nextInOrder);

        //                if (comparisonResult >= 0 && !studentsSorted.ContainsKey(studentWithScore.Key))
        //                {
        //                    nextInOrder = studentWithScore;
        //                    isSorted = false;
        //                }
        //            }
        //            else
        //            {
        //                if (!studentsSorted.ContainsKey(studentWithScore.Key))
        //                {
        //                    nextInOrder = studentWithScore;
        //                    isSorted = false;
        //                }
        //            }
        //        }

        //        if (!isSorted)
        //        {
        //            studentsSorted.Add(nextInOrder.Key, nextInOrder.Value);
        //            valuesTaken++;
        //            nextInOrder = new KeyValuePair<string, List<int>>();
        //        }

        //        if (valuesTaken >= takeCount)
        //        {
        //            break;
        //        }
        //    }

        //    return studentsSorted;
        //}
    }
}
