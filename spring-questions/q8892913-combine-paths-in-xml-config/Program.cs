using System;
using NUnit.Framework;
using Spring.Context.Support;

namespace q8892913
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = System.IO.Path.Combine(@"c:\dev", "temp");
            Console.WriteLine(path);
            Console.ReadLine();
        }
    }

    [TestFixture]
    public class DictionaryTests
    {
        [Test, Ignore]
        public void Main()
        {
            var ctx = new XmlApplicationContext("objects.xml");
            var o = (MyClass)ctx.GetObject("MyObject");

            Assert.AreEqual(@"c:\\dev\\temp", o.Path);

            Console.WriteLine(o);
        }
    }

    public class MyClass
    {
        public string Path { get; set; }
    }

}
