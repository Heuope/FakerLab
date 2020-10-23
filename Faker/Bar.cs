using System;
using System.Collections.Generic;
using System.Text;

namespace Faker
{
    class Bar
    {
        public string a;
        public int b;

        public Foo foo;

        public Bar(string a, int b, Foo foo)
        {
            this.foo = foo;
            this.a = a;
            this.b = b;
        }
    }
}
