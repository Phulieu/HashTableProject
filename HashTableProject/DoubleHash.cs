using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTableProject
{
    internal class DoubleHash<K, V> : HashTable<K, V> where K : IComparable<K> where V : IComparable<V>
    {
        protected override int GetIncrement(int iAttempt, K key)
        {
            return (1+ Math.Abs(key.GetHashCode()) % (HTSize -1))*iAttempt;
        }
    }
}
