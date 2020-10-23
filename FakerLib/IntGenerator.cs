using System;
using System.Collections.Generic;
using System.Text;

namespace FakerLib
{
    class IntGenerator : IGenerator
    {
        public object Generate()
        {
            var rand = new Random();
            return rand.Next(1, 100);
        }
    }
}
