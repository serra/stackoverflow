using System;
using NUnit.Framework;
using Spring.Context.Support;
using XmlConfig.StringInjection;

namespace XmlConfig.AutoWireByName
{
    [TestFixture]
    public class AutoWireByName
    {
        [Test]
        public void LetsAutoWireByName()
        {
            var ctx = new XmlApplicationContext("AutoWireByName/objects.xml");
            Texte texte = null;
            texte = (Texte)ctx.GetObject("texte");
            texte.Print();
        } 
    }

    class Texte
    {
        private String _t;
        private Description _desc;
        public String T
        {
            get { return _t; }
            set { _t = value; }
        }
        public Description Desc
        {
            get { return _desc; }
            set { _desc = value; }
        }
        public void Print()
        {
            Console.WriteLine("text in object: " + _t);
            Console.WriteLine("text description: " + _desc.D);
        }
    }

    class Description
    {
        private String _d;
        public String D
        {
            get { return _d; }
            set { _d = value; }
        }
    }
}