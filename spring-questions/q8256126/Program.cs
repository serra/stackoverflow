using System;
using System.Collections.Generic;
using NUnit.Framework;
using Spring.Context.Support;

namespace q8256126
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

            var myObject2 = new MyClass() { Name = "MyObject2"};
            
            ctx.ObjectFactory.RegisterSingleton("MyObject2", myObject2);
            
            var o = ctx.GetObject("MyObject");

            var o2 = (MyClass)ctx.GetObject("MyObject2");

            Assert.AreEqual("MyObject2", o2.Name);

        }
    }

    public class MyClass
    {
        public string Name { get; set; }
    }

}

/*
 
 Spring documentation on http://springframework.net/docs/1.3.1/reference/html/objects.html says:

> "In addition to object definitions which contain information on how to
> create a specific object, the IApplicationContext implementations also
> permit the registration of existing objects that are created outside
> the container, by users. This is done by accessing the
> ApplicationContext's IObjectFactory via the property ObjectFactory
> which returns the IObjectFactory implementation
> DefaultListableObjectFactory. DefaultListableObjectFactory supports
> registration through the methods RegisterSingleton(..) and
> RegisterObjectDefinition(..)."

I'm trying to access the `ObjectFactory` object after doing the following:

    var context = ContextRegistry.GetContext();

But there is no `ObjectFactory` property. I'm using Spring.Net v1.3.1.20711 and have `Spring.Core` referenced in my project.

What am I missing?
 
 
 */
