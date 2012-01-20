using System;
using NUnit.Framework;
using Spring.Context;
using Spring.Context.Support;

namespace q8805267
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = GetApplicationContext();
            var o1 = ctx.GetObject("MyObject");
            Console.WriteLine(o1);

            Console.WriteLine("Make your changes, then hit Enter to continue.");
            Console.ReadLine();

            ctx.Refresh();
            
            var o2 = ctx.GetObject("MyObject");

            Console.WriteLine(o2);

            ctx.Dispose();

            Console.WriteLine("Hit Enter to exit ...");
            Console.ReadLine();

        }

        public static XmlApplicationContext GetApplicationContext()
        {
            return new XmlApplicationContext("objects.xml");
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Main()
        {
            var ctx = Program.GetApplicationContext();
            var o = ctx.GetObject("MyObject");
            Console.WriteLine(o);
        }
    }

    public class MyClass : IDisposable
    {
        public String Name { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public void Dispose()
        {
            Console.WriteLine("Dispose was called on {0}.", Name);
        }
    }

}
