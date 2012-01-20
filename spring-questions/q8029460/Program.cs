using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using NUnit.Framework;
using Spring.Aop;
using Spring.Context.Support;

namespace q8029460
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = new q8029460Tests();
            t.Main();
            Console.ReadLine();
        }
    }

    [TestFixture]
    public class q8029460Tests
    {
        [Test]
        public void Main()
        {
            var ctx = ContextRegistry.GetContext();
            Console.WriteLine("Context is of type: {0}", ctx.GetType());
            var ohI = (MyClass)ctx["mySomeClass"];
            ohI.WantToLogThisCall();
            Console.WriteLine(ohI);
        }
    }

    public class MyClass
    {
        [LogCall]
        public virtual void WantToLogThisCall()
        {
            Console.WriteLine("Should have been logged before.");
        }
    }

    public class LogCallInterceptor : IMethodBeforeAdvice
    {
        public void Before(MethodInfo method, object[] args, object target)
        {
            Console.WriteLine("Before calling method: {0}", method.Name);
        }
    }

    [Serializable]
    [AttributeUsage(AttributeTargets.Method)]
    public class LogCallAttribute : Attribute
    {
    }

}
