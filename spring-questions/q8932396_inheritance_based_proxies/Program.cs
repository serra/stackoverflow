using System;
using System.Collections.Generic;
using NUnit.Framework;
using Spring.Context.Support;


namespace q8932396_inheritance_based_proxies
{
    /// <summary>
    /// http://stackoverflow.com/questions/8932396/why-are-pure-inheritance-based-proxies-bad-in-aop
    /// </summary>
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
            var o = (MyClass)ctx.GetObject("MyObject");

            o.Method1();

            Console.WriteLine(o);
        }
    }

    public class MyClass
    {
        public virtual void Method1()
        {
            Console.WriteLine("1: This is Method1()");

            Method2();
        }

        public virtual void Method2()
        {
            Console.WriteLine("2: This is Method2()");
        }
    }

}
