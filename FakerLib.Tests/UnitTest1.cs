using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakerLib;
using System.Collections.Generic;

namespace FakerLib.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private Faker _faker;

        [TestInitialize]
        public void Setup()
        {
            var context = new GeneratorContext();

            context.AddNewGenerator(new IntGenerator());
            context.AddNewGenerator(new StringGenerator());
            context.AddNewGenerator(new ListGenerator());
            context.AddNewGenerator(new DateTimeGenerator());
            context.LoadNewGenerator(@"C:\Users\konst\Desktop\FakerLab\DoubleGeneratorLib\bin\Debug\netcoreapp3.1\DoubleGeneratorLib.dll");
            context.LoadNewGenerator(@"C:\Users\konst\Desktop\FakerLab\FloatGeneratorLib\bin\Debug\netcoreapp3.1\FloatGeneratorLib.dll");

            _faker = new Faker(context);
        }

        class Bar
        {
            public string str { get; set; }
        }

        class Foo
        {
            public Bar bar;
        }

        class Zoo
        {
            private int Data;
            private bool flag = false;

            public Zoo(int data)
            {
                Data = data;
                flag = true;
            }

            public bool IsSetData()
            {
                return flag;
            }
        }

        class ZooTwoConstuctors
        {
            private int Data;
            private bool constructor1 = false;
            private bool constructor2 = false;

            public ZooTwoConstuctors()
            {
                constructor1 = true;
            }

            public ZooTwoConstuctors(int data)
            {
                Data = data;
                constructor2 = true;
            }

            public bool IsSetData()
            {
                return constructor2;
            }
        }

        class Rec1
        {
            public Rec2 A;
        }

        class Rec2
        {
            public Rec1 B;
        }

        class PrivateConstructor
        {
            private PrivateConstructor()
            {

            }
        }

        [TestMethod]
        public void IsGenerateInteger()
        {
            var actual = _faker.Create<int>();
            int notExpected = 0;
            Assert.AreNotEqual(notExpected, actual);
        }

        [TestMethod]
        public void IsGenerateStringProperty()
        {
            Bar bar = _faker.Create<Bar>();
            Assert.AreEqual(true, bar.str.Length > 0);
        }

        [TestMethod]
        public void IsGenerateObjectField()
        {
            Foo foo = _faker.Create<Foo>();
            Assert.AreNotEqual(null, foo.bar);
        }

        [TestMethod]
        public void PrivateFieldTest()
        {
            Zoo zoo = _faker.Create<Zoo>();
            Assert.AreEqual(true, zoo.IsSetData());
        }

        [TestMethod]
        public void TwoConstructorsTest()
        {
            ZooTwoConstuctors foo = _faker.Create<ZooTwoConstuctors>();
            Assert.AreEqual(true, foo.IsSetData());
        }

        [TestMethod]
        public void ReccursionTest()
        {
            Rec1 rec = _faker.Create<Rec1>();
            Assert.AreEqual(null, rec.A.B);
        }

        [TestMethod]
        public void GenericListTest()
        {
            List<int> intList = _faker.Create<List<int>>();
            Assert.AreEqual(true, intList.Count > 0);
            Assert.AreEqual(true, intList[0].GetType() == typeof(int));
        }

        [TestMethod]
        public void GenericListListTest()
        {
            List<List<int>> intList = _faker.Create<List<List<int>>>();
            Assert.AreEqual(true, intList.Count > 0);
            Assert.AreEqual(true, intList[0].Count > 0);
            Assert.AreEqual(true, intList[0].GetType() == typeof(List<int>));
            Assert.AreEqual(true, intList[0][0].GetType() == typeof(int));
        }
    }
}
