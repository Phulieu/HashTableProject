using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTableProject
{
    internal class Linear<K,V> : HashTable<K,V> where K: IComparable<K> where V: IComparable<V>
    {
        protected override int GetIncrement(int iAttempt, K key)
        {
            int iIncrement = 1;
            return iIncrement*iAttempt;
        }
    }
}
