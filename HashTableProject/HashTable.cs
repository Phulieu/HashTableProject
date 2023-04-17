using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTableProject
{
    public abstract class HashTable<K,V> where K : IComparable<K> where V: IComparable<V>
    {
        protected object[] oHashTable;
        protected int iCount;
        protected double dLoadFactor = 0.7;
        protected int iNumCollisions = 0;


        public int Count {get => iCount;}

        public int NumCollisions {get => iNumCollisions;}

        public int HTSize
        {
            get
            {
                return oHashTable.Length;
            }
        }

        protected abstract int GetIncrement(int iAttempt, K key);  

        private PrimeNumber currTableSize = new PrimeNumber();

        protected HashTable()
        {
            oHashTable = new object[currTableSize.GetNextPrime()];
        }

        protected int HashFunction(K key)
        {
            return Math.Abs(key.GetHashCode() % HTSize);
        }

        public void Add(K key, V value)
        {
            int iAttempt = 1;
            int iInitialHash = HashFunction(key);
            int iCurrentLocation = iInitialHash;
            KeyValue<K,V> kvNew = new KeyValue<K,V>(key, value);

            // find an empty location to add kvNew
            while (oHashTable[iCurrentLocation] != null)
            {
                //is it a duplicate
                if (oHashTable[iCurrentLocation].GetType() == typeof(KeyValue<K, V>))
                {
                    KeyValue<K,V> kv = (KeyValue<K,V>)oHashTable[iCurrentLocation];
                    if (kv.Equals(kvNew))
                    {
                        throw new ApplicationException("The item already exits");
                    }

                } else
                // if this is a deleted placeholder , break to go ahead and insert there
                {
                    break;
                }

                iCurrentLocation = iInitialHash + GetIncrement(iAttempt++, key);
                iCurrentLocation %= HTSize;

                iNumCollisions++;
            }

            //insert at iCurrentLocation
            oHashTable[iCurrentLocation] = kvNew;
            iCount++;

            if (IsOverloaded())
            {
                ExpandHashTable();
            }

        }


        public V Get(K key)
        {
            V vReturn = default(V);
            int iAttempt = 1;
            int iInitialHash = HashFunction(key);
            int iCurrentLocation = iInitialHash;
            bool found = false;
        
            while (!found &&  oHashTable[iCurrentLocation] != null)
            {
                if (oHashTable[iCurrentLocation].GetType() == typeof(KeyValue<K, V>))
                {
                    KeyValue<K, V> kv = (KeyValue<K, V>)oHashTable[iCurrentLocation];
                    if (kv.Key.CompareTo(key) == 0)
                    {
                        vReturn = kv.Value;
                        found = true;
                    }

                }

                iCurrentLocation = iInitialHash + GetIncrement(iAttempt++, key);
                iCurrentLocation %= HTSize;
            }

            if (!found)
            {
                throw new ApplicationException("Key not found");
            }

            return vReturn;



        }

        public void Remove(K key)
        {
            int iAttempt = 1;
            int iInitialHash = HashFunction(key);
            int iCurrentLocation = iInitialHash;
            bool found = false;

            while (!found && oHashTable[iCurrentLocation] != null)
            {
                if (oHashTable[iCurrentLocation].GetType() == typeof(KeyValue<K, V>))
                {
                    KeyValue<K, V> kv = (KeyValue<K, V>)oHashTable[iCurrentLocation];
                    if (kv.Key.CompareTo(key) == 0)
                    {
                        //delete if we find what we're looking for
                        oHashTable[iCurrentLocation] = new Deleted();
                        found = true;
                        iCount--;
                    }

                }

                iCurrentLocation = iInitialHash + GetIncrement(iAttempt++, key);
                iCurrentLocation %= HTSize;
            }

            if (!found)
            {
                throw new ApplicationException("Key not found");
            }
        }

        private bool IsOverloaded()
        {
            return (iCount /(double)HTSize > dLoadFactor); 
        }

        private void ExpandHashTable()
        {
            object[] oOldTable = oHashTable;
            oHashTable = new object[currTableSize.GetNextPrime()];
            iCount = 0;
            iNumCollisions = 0;

            for (int i = 0; i < oOldTable.Length; i++)
            {
                if(oOldTable[i] != null)
                {
                    if (oOldTable[i].GetType() == typeof(KeyValue<K,V>))
                    {
                        KeyValue<K,V> kv = (KeyValue<K,V>)oOldTable[i];
                        this.Add(kv.Key, kv.Value);
                    }
                }
            }

        }

    }
}
