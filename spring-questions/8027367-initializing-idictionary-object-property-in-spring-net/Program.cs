using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Spring.Context.Support;

namespace q8027367
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }

    [TestFixture]
    public class DictionaryTests
    {
        [Test]
        public void Main()
        {
            var ctx = new XmlApplicationContext("objects.xml");
            var o = ctx.GetObject("MyObject");
            Console.WriteLine(o);
        }
    }
    
    public class MyObjectClass
    {
        public IDictionary<string, string> Params { get; set; }
    }

}
