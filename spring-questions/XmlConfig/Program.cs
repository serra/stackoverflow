using System;
using System.Collections.Generic;
using NUnit.Framework;
using Spring.Context.Support;

namespace XmlConfig
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
            var ctx = new XmlApplicationContext("objects.xml");
            

            var list1 = ctx["list1"];
            var list2 = ctx["list2"];
            var dic1 = (Dictionary<string, List<string>>)ctx["dic1"];
            var dic0 = (Dictionary<string, Dictionary<string, List<string>>>)ctx["dic0"];
            var o = ctx.GetObject("MyObject");

            Console.WriteLine(o);
        }
    }

    public class MyClass
    {
        public Dictionary<string, Dictionary<string, List<string>>> ContextMenuModel { get; set; }
    }

}
