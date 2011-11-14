using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Spring.Context.Support;

namespace q8081112_inject_hashtable_in_spring_net
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

            Assert.IsNotNull(o.MyHashTable);

            Assert.AreEqual("value1", o.MyHashTable["key1"]);

            Console.WriteLine(o);
        }
    }

    public class MyClass
    {
        public Hashtable MyHashTable { get; set; }
    }

}
