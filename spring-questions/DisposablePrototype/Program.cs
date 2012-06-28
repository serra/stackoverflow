using System;
using System.Collections.Generic;
using NUnit.Framework;
using Spring.Context.Support;
using Spring.Objects.Factory;

namespace DisposablePrototype
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
            var o1 = ctx.GetObject("SingletonObject");
            var o2 = ctx.GetObject("ProtoTypeObject");
            ctx.Dispose();
        }
    }

    public class MyClass : IDisposable, IObjectNameAware
    {
        private string _name;

        public void Dispose()
        {
            Console.WriteLine("Disposing {0}!", _name);
        }

        public string ObjectName
        {
            set { _name = value; }
        }
    }

}
