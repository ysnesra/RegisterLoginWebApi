﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Utilities.Toolkit
{
    public static class RandomGenerator
    {
        public static string RandomOneTimePassword(int min = 100000, int max = 999999)
        {
            var random = new Random();
            return random.Next(min, max).ToString();
        }
    }
}
