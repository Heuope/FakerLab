using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NLog;
using System;
using System.IO;

namespace FakerLib.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private Faker _faker;
        private NLog.Logger _logger = LogManager.GetCurrentClassLogger();

        [TestInitialize]
        public void Setup()
        {
            var context = new GeneratorContext();

            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;

            context.AddNewGenerator(new IntGenerator());
            context.AddNewGenerator(new StringGenerator());
            context.AddNewGenerator(new ListGenerator());
            context.AddNewGenerator(new DateTimeGenerator());

            _logger.Info("Start loading dll");

            try
            {
                context.LoadNewGenerator(@$"{path}\DoubleGeneratorLib\bin\Debug\netcoreapp3.1\DoubleGeneratorLib.dll");
                context.LoadNewGenerator(@$"{path}\FloatGeneratorLib\bin\Debug\netcoreapp3.1\FloatGeneratoLib.dll");
            }
            catch (Exception exception)
            {
                _logger.Debug(exception, "Failed to load dll");
            }

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
            int expected = 0;

            if (actual != expected)
                _logger.Info($"IsGenerateInteger complete succesfully => {expected} != {actual}");
            else
                _logger.Info($"IsGenerateInteger complete not succesfully => {expected} == {actual}");

            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void IsGenerateStringProperty()
        {
            Bar bar = _faker.Create<Bar>();

            var expected = true;
            var actual = bar.str.Length > 0;

            if (actual == expected)
                _logger.Info($"IsGenerateStringProperty complete succesfully => {expected} == {actual}");
            else
                _logger.Info($"IsGenerateStringProperty complete not succesfully => {expected} != {actual}");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsGenerateObjectField()
        {
            Foo foo = _faker.Create<Foo>();

            Bar expected = null;
            var actual = foo.bar;

            if (actual == expected)
                _logger.Info($"IsGenerateObjectField complete succesfully => {expected} == {actual}");
            else
                _logger.Info($"IsGenerateObjectField complete not succesfully => {expected} != {actual}");

            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void PrivateFieldTest()
        {
            Zoo zoo = _faker.Create<Zoo>();

            var expected = true;
            var actual = zoo.IsSetData();

            if (actual == expected)
                _logger.Info($"PrivateFieldTest complete succesfully => {expected} == {actual}");
            else
                _logger.Info($"PrivateFieldTest complete not succesfully => {expected} != {actual}");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TwoConstructorsTest()
        {
            ZooTwoConstuctors foo = _faker.Create<ZooTwoConstuctors>();

            var expected = true;
            var actual = foo.IsSetData();

            if (actual == expected)
                _logger.Info($"TwoConstructorsTest complete succesfully => {expected} == {actual}");
            else
                _logger.Info($"TwoConstructorsTest complete not succesfully => {expected} != {actual}");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ReccursionTest()
        {
            Rec1 rec = _faker.Create<Rec1>();

            object expected = null;
            var actual = rec.A.B;

            if (actual == expected)
                _logger.Info($"ReccursionTest complete succesfully => {expected} == {actual}");
            else
                _logger.Info($"ReccursionTest complete not succesfully => {expected} != {actual}");

            Assert.AreEqual(null, rec.A.B);
        }

        [TestMethod]
        public void GenericListTest()
        {
            List<int> intList = _faker.Create<List<int>>();

            var expected = true;
            var actual = intList.Count > 0;

            if (actual == expected)
                _logger.Info($"GenericListTest complete succesfully => {expected} == {actual}");
            else
                _logger.Info($"GenericListTest complete not succesfully => {expected} != {actual}");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GenericListListTest()
        {
            List<List<int>> intList = _faker.Create<List<List<int>>>();

            var expected = true;
            var actualFirstLevelCount = intList.Count > 0;
            var actualSecondlevelCount = intList[0].Count > 0;

            if (actualFirstLevelCount == expected && actualSecondlevelCount == expected)
                _logger.Info($"GenericListListTest complete succesfully => {expected} == {actualFirstLevelCount} and {expected} == {actualSecondlevelCount}");
            else
                _logger.Info($"GenericListListTest complete not succesfully => {expected} != {actualSecondlevelCount} or {expected} != {actualSecondlevelCount}");

            Assert.AreEqual(true, intList.Count > 0);
            Assert.AreEqual(true, intList[0].Count > 0);
        }
    }
}
