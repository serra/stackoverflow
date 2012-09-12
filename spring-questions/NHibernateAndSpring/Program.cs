using System;
using System.Collections.Generic;
using NUnit.Framework;
using Spring.Context.Support;

namespace NHibernateAndSpring
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
            var ctx = new XmlApplicationContext("objects.xml");
            var o = ctx.GetObject("MyObject");
            Console.WriteLine(o);
        }
    }

    public class MyClass
    {

    }

}
