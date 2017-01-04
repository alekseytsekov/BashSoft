using System;
using System.Collections.Generic;

namespace BashSoft.Contracts
{
    public interface ICourse : IComparable<ICourse>
    {
        IReadOnlyDictionary<string, IStudent> StudentsByName { get; }
        string Name { get; }
        void EnrollStudent(IStudent student);
    }
}
