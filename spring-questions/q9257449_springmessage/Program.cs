using System;
using System.Collections.Generic;
using NUnit.Framework;
using Spring.Context.Support;

namespace q9257449_springmessage
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
            var o = (MyClass)ctx.GetObject("MyObject");
            Console.WriteLine(o);

            var ms = (ResourceSetMessageSource)ctx["messageSource"];

            Assert.AreEqual("Hello mr. Anderson", ms.GetMessage("HelloMessage", "mr.", "Anderson"));

            Assert.AreEqual("Hello mr. Anderson", o.Greeting);
        }
    }

    public class MyClass
    {
        public string Greeting { get; set; }
    }

}
