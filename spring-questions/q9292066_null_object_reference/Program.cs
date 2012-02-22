using System;
using System.Collections.Generic;
using NUnit.Framework;
using Spring.Context.Support;
using Spring.Objects.Factory;

namespace q9292066_null_object_reference
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
        [ExpectedException(typeof(FactoryObjectNotInitializedException))]
        public void UsingNullFactoryObjectIsNotAnOption()
        {
            var ctx = new XmlApplicationContext("objects.xml", "objects-ref1-as-nullfactoryobject.xml");

            var o = (MyClass)ctx.GetObject("MyObject");
            Assert.IsNull(o.Prop);

            var o2 = (MyClass)ctx.GetObject("MySecondObject");
            Assert.IsNotNull(o2.Prop);

        }

        [Test]
        public void UsingNullFactoryObjectIsAnOptionIfWeMassageContextToAllowForNullReferences()
        {
            var ctx = new XmlApplicationContext("objects.xml", "objects-ref1-as-nullobject.xml");

            var o2 = (MyClass)ctx.GetObject("MySecondObject");
            Assert.IsNotNull(o2.Prop);

            var o = (MyClass)ctx.GetObject("MyObject");
            Assert.IsNotNull(o.Prop);
            Assert.IsInstanceOf<IMyOtherInterface>(o.Prop);
            Assert.IsInstanceOf<MyOtherClassNullObject>(o.Prop);

        }
    }

    public class MyClass
    {
        public IMyOtherInterface Prop { get; set; }

        public MyClass(IMyOtherInterface ref1)
        {
            Prop = ref1;
        }
    }


    public interface IMyOtherInterface
    {
    }

    public class MyOtherClass : IMyOtherInterface
    {
    }

    public class MyOtherClassNullObject : IMyOtherInterface
    {
    }

    public class NullFactoryObject : IFactoryObject
    {
        public object GetObject()
        {
            return null;
        }

        public Type ObjectType
        {
            get { return typeof(MyOtherClass); }
        }

        public bool IsSingleton
        {
            get { return true; }
        }
    }

}
