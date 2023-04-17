﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTableProject
{
    internal class PrimeNumber
    {
        int iCurrent = -1;
        int[] iPrimes = { 5, 11, 19, 41, 79, 163, 317, 641, 1201, 2399, 4801, 9733 };

        public int GetNextPrime()
        {
            iCurrent++;
            return iPrimes[iCurrent];
        }
    }
}
