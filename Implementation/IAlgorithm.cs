﻿using App.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Algorithms
{
    interface IAlgorithm
    {
        IAlgorithmResult Find(string pattern);
    }
}
