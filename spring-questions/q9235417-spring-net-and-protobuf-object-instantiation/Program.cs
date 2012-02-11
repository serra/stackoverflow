using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using ProtoBuf;
using Spring.Context.Support;

namespace q9235417
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
        const string FileName = "model.bin";

        [SetUp]
        public void SetUp()
        {
            var mdl = new Model {Name = "Marijn"};
            using (var file = File.Create(FileName))
            {
                Serializer.Serialize(file, mdl);
            }
        }

        [TearDown]
        public void TearDown()
        {
            if(File.Exists(FileName))
                File.Delete(FileName);
        }

        [Test]
        public void Main()
        {
            var ctx = new XmlApplicationContext("objects.xml");
            var o = ctx.GetObject("MyObject");
            Console.WriteLine(o);

            Model mdl;

            using (var file = File.OpenRead(FileName))
            {
                mdl = Serializer.Deserialize<Model>(file);
            }
            
            Assert.AreEqual("Marijn", mdl.Name);

            ctx.ConfigureObject(mdl, "MyModel");

            Assert.NotNull(mdl.DataFactory);

            mdl.DataFactory.WriteHello();

            Model mdl2;

            using (var file = File.OpenRead(FileName))
            {
                mdl2 = Serializer.Deserialize<Model>(file);
            }

            Assert.AreEqual("Marijn", mdl2.Name);

            ctx.ConfigureObject(mdl2, "MyModel");

            Assert.NotNull(mdl2.DataFactory);

            mdl2.DataFactory.WriteHello();

        }
    }

    public interface IHelloWriter
    {
        void WriteHello();
    }

    public class MyClass : IHelloWriter
    {
        public void WriteHello()
        {
            Console.WriteLine("Hi!");
        }
    }


    [ProtoContract]
    public class Model
    {
        [ProtoMember(1)]
        public String Name { get; set; }

        [ProtoIgnore]
        public IHelloWriter DataFactory { get; set; }
    }

}
