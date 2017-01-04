using System;
using System.Collections.Generic;

namespace BashSoft.Contracts
{
    public interface ISimpleOrderBag<T>: IEnumerable<T> where T : IComparable<T>
    {
        void Add(T element);
        void AddAll(ICollection<T> elements);
        int Size { get; }
        string JoinWith(string joiner);
        bool Remove(T element);
        int Capacity { get; }
    }
}
