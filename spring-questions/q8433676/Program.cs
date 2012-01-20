using System;
using NUnit.Framework;
using Spring.Context.Support;

namespace MyNameSpace
{
    class Program
    {
        static void Main(string[] args)
        {
            new Tests().Main();
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Main()
        {
            var ctx = new XmlApplicationContext("objects.xml");
            
            var o = (MyClass)ctx.GetObject("MyObject");
            Assert.AreEqual(typeof(MySecondClass), o.TheType);

            var o2 = (MyClass)ctx.GetObject("MyObject2");
            Assert.AreEqual(typeof(MySecondClass), o2.TheType);

            var o3 = (MyClass)ctx.GetObject("MyObject3");
            Assert.AreEqual(typeof(MySecondClass), o3.TheType);
        }

        [Test]
        public void TypeConstructorTest()
        {
            var obj = new MyClass(typeof (MySecondClass));
            Assert.AreEqual(typeof (MySecondClass), obj.TheType);
        }
    }

    public class MyClass
    {
        public Type TheType { get; private set; }

        public MyClass(Type aType)
        {
            TheType = aType;
        }
    }

    public class MySecondClass
    {
    }

}
