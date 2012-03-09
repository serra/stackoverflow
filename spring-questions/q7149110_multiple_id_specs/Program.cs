using System;
using System.Collections.Generic;
using NUnit.Framework;
using Spring.Context.Support;

namespace q7149110_multiple_id_specs
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Main()
        {
            var ctx = new XmlApplicationContext("objects.xml","objects2.xml");
            var o = (MyClass)ctx.GetObject("MyObject");

            Assert.AreEqual("From objects2.xml", o.Name);
            Console.WriteLine(o);
        }
    }

    public class MyClass
    {
        public string Name { get; set; }
    }

}
