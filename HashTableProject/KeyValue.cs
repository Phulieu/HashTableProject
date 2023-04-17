using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTableProject
{
    internal class KeyValue<K, V> where K : IComparable<K> where V : IComparable<V>
    {
        K key;
        V vValue;

        public KeyValue(K key, V value)
        {
            this.key = key;
            this.vValue = value;
        }

        public K Key
        {
            get { return key; }
        }

        public V Value { get { return vValue; } }

        public override bool Equals(object obj)
        {
           KeyValue<K, V> kv = (KeyValue<K,V>) obj;
            return this.Key.CompareTo(kv.Key) == 0;

        }
    }
}
