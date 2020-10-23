using System;
using System.Collections.Generic;
using System.Text;

namespace Faker
{
    class Zoo
    {
        public Bar bar;
        public DateTime dateTime;
        public float fl;
        public string str;
        public int t { get; set; }

        public Zoo(float fl)
        {
            this.fl = fl;
        }

        public Zoo(float fl, int s)
        {
            this.fl = fl;
            t = s;
        }
    }
}
