using System;
using System.Collections.Generic;
using System.Text;

namespace Faker
{
    class Bar
    {
        public string a;
        public int b;
        public List<List<int>> e; 

        public Foo foo;

        public Bar(string a, int b, Foo foo, List<List<int>> e)
        {
            this.foo = foo;
            this.a = a;
            this.b = b;
            this.e = e;
        }
    }
}
