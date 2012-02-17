using System;
using System.Collections.Generic;
using NUnit.Framework;
using Spring.Context.Support;
using Spring.Objects.Factory;

namespace q9292066_null_object_reference
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
        }
    }

    [TestFixture]
    public class DictionaryTests
    {
        [Test]
        [ExpectedException(typeof(FactoryObjectNotInitializedException))]
        public void Main()
        {
            var ctx = new XmlApplicationContext("objects.xml", "objects2.xml");

            var o = (MyClass)ctx.GetObject("MyObject");
            Assert.IsNull(o.Prop);

            var o2 = (MyClass)ctx.GetObject("MySecondObject");
            Assert.IsNotNull(o2.Prop);

        }
    }

    public class MyClass
    {
        public MyOtherClass Prop { get; set; }

        public MyClass(MyOtherClass ref1)
        {
        }
    }

    public class MyOtherClass
    {
    }


    public class NullFactoryObject : IFactoryObject
    {
        public object GetObject()
        {
            return null;
        }

        public Type ObjectType
        {
            get { return typeof(MyOtherClass); }
        }

        public bool IsSingleton
        {
            get { return true; }
        }
    }

}
