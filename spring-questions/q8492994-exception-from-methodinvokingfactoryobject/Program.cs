using System;
using NUnit.Framework;
using Spring.Context.Support;

namespace q8492994
{
    internal class Program
    {
        private static void Main(string[] args)
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
            XmlApplicationContext ctx;
            try
            {
                ctx = new XmlApplicationContext("objects.xml");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Assert.Pass("This exception is expected.");
                throw;
            }
        }
    }

    public class MyClass
    {
    }

    public class ExceptionThrowingClass
    {
        public void MethodThatThrows()
        {
            throw new Exception("Unexpected exception ... how to catch it?");
        }
    }
}