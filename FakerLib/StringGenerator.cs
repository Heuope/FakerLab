using System;
using System.Collections.Generic;
using System.Text;

namespace FakerLib
{
    class StringGenerator : IGenerator
    {
        public object Generate()
        {
            var str = new StringBuilder();
            var rand = new Random();

            for (int i = 0; i < rand.Next(1, 50); i++)
            {
                str.Append((char)rand.Next(1, 255));
            }

            return str.ToString();
        }
    }
}
