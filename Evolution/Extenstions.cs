using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution
{
    public static class Extenstions
    {
        public static float NextFloat(this Random random, float minimum, float maximum)
        {
            var limitedRandom = ((float)random.NextDouble()) * (maximum - minimum) + minimum;
            return limitedRandom;
        }
    }
}
