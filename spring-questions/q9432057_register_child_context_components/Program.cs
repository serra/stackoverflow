using System;
using System.Collections.Generic;
using NUnit.Framework;
using Spring.Context.Support;

namespace q9432057_register_child_context_components
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
            var ctx = ContextRegistry.GetContext("ParentContext"); // load from app.config

            var ctx1 = ContextRegistry.GetContext("ChildContext1");
            var ctx2 = ContextRegistry.GetContext("ChildContext2");

            Assert.AreEqual(ctx, ctx1.ParentContext);
            Assert.AreEqual(ctx, ctx2.ParentContext);

            //Assert.AreEqual(3, ctx.GetObjectsOfType(typeof(IComponent)).Count);

            Assert.IsInstanceOf<ComponentRepository>(ctx1["repo"]);
            Assert.IsInstanceOf<ComponentRepository>(ctx2["repo"]);

            var o = ctx.GetObject("MyObject");
            Console.WriteLine(o);
        }

    }

    public interface IComponent
    {
        string Name { get; }
    }

    public class MyClass : IComponent
    {
        public String Name { get; set; }

        public virtual object Register()
        {
            return null;
        }
    }

    public class ComponentRepository
    {
        public void Register(IComponent component)
        {
            Console.WriteLine(component.Name);
        }
    }

}
