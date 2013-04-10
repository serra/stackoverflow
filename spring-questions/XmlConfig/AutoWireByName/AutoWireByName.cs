using System;
using NUnit.Framework;
using Spring.Context.Support;

// http://stackoverflow.com/questions/15900061/spring-net-autowire-tag-does-not-work/15906027?noredirect=1#comment22674406_15906027

namespace XmlConfig.AutoWireByName
{
    [TestFixture]
    public class AutoWireByName
    {
        [Test]
        public void LetsAutoWireByName()
        {
            var ctx = new XmlApplicationContext("AutoWireByName/objects.xml");
            var texte = (Texte)ctx.GetObject("texte");
            texte.Print();
        } 
    }

    class Texte
    {
        public string T { get; set; }
        public Description Desc { get; set; }

        public void Print()
        {
            Console.WriteLine("text in object: " + T);
            Console.WriteLine("text description: " + Desc.D);
        }
    }

    class Description
    {
        public string D { get; set; }
    }
}