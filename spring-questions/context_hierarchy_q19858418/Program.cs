using System;
using System.Collections.Generic;
using NUnit.Framework;
using Spring.Context.Support;

namespace context_hierarchy_q19858418
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = ContextRegistry.GetContext("Parent");
            var o1 = (MyClass)ctx.GetObject("MyObject");
            var o3 = (MyClass)ctx.GetObject("MyObject3");
            
            var ctx2 = ContextRegistry.GetContext("child");
            var o2 = (MyClass)ctx2.GetObject("MyObject2");
            Console.WriteLine("{0} name: '{1}'", o1, o1.Name);
            Console.WriteLine("{0} name: '{1}'", o3, o3.Name);
            Console.WriteLine("{0} name: '{1}'", o2, o2.Name);
            Console.ReadLine();
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Main()
        {
            var ctx = ContextRegistry.GetContext();
            var o = ctx.GetObject("MyObject");
            Console.WriteLine(o);
        }
    }

    public class MyClass
    {
        public string Name { get; set; }
    }

}
