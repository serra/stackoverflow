using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Serialization;
using NUnit.Framework;
using Spring.Aop;
using Spring.Aop.Framework;
using Spring.Context.Support;

namespace q8121697
{
    class Program
    {
        static void Main(string[] args)
        {
            var myObject = new MyClass();

            // this creates a dynamic assembly 'Spring.Proxy'
            var factory = new ProxyFactory(myObject);
            factory.AddAdvice(new LogCallInterceptor());
            var myProxy = (IMyInterface)factory.GetProxy();
            myProxy.DoSomething();
            
            Console.WriteLine(Assembly.GetAssembly(myProxy.GetType()).FullName);
            Console.WriteLine();

            // this creates a dynamic assembly 'Spring.Proxy'
            var myObject2 = new MyClass2();
            var factory2 = new ProxyFactory(myObject2);
            factory2.AddAdvice(new LogCallInterceptor());
            factory2.ProxyTargetType = true;
            var myProxy2 = (MyClass2)factory2.GetProxy();
            myProxy2.DoSomething();
            Console.WriteLine(Assembly.GetAssembly(myProxy2.GetType()).FullName);

            Console.WriteLine();

            // this creates a dynamic assembly for the xml serializer (the strnge assembly name from question?)
            var s = new XmlSerializer(typeof(MyClass));
            s.Serialize(Console.Out, myObject);
            Console.WriteLine();

            Console.ReadLine();
        }
    }

    

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Main()
        {
            //var interceptorMock = new Moq.Mock<IMethodBeforeAdvice>();
            //interceptorMock.Setup(i => i.Before(It.IsAny<MethodInfo>(), It.IsAny<object[]>(), It.IsAny<object>()));
        }
    }

    public interface IMyInterface
    {
        void DoSomething();
    }

    public class MyClass : IMyInterface
    {
        public string Name { get; set; }

        public virtual void DoSomething()
        {
            Console.WriteLine("Doing something ... Done.");
        }
    }

    public class MyClass2 
    {
        public virtual string Name { get; set; }

        public virtual void DoSomething()
        {
            Console.WriteLine("Doing something ... Done.");
        }
    }

    public class LogCallInterceptor : IMethodBeforeAdvice
    {
        public void Before(MethodInfo method, object[] args, object target)
        {
            Console.WriteLine("Before calling method: {0}", method.Name);
        }
    }

}
