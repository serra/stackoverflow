using System;
using System.Collections.Generic;
using NUnit.Framework;
using Spring.Context.Support;

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
        public void Main()
        {
            var ctx = ContextRegistry.GetContext(); //new XmlApplicationContext("objects.xml");
            var o = ctx.GetObject("MyObject");
            Console.WriteLine(o);
        }
    }

    public class MyClass
    {
        public MyClass()
        {
        }

        public MyClass(MyClass ref1)
        {
        }
    }

}
