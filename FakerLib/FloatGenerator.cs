using System;
using System.Collections.Generic;
using System.Text;

namespace FakerLib
{
    class FloatGenerator : IGenerator
    {
        public object Generate()
        {
            var rand = new Random();

            return (float)(rand.NextDouble() * (1 - 0.1) + 0.1);
        }
    }
}
