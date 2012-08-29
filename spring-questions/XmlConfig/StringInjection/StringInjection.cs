using NUnit.Framework;
using Spring.Context.Support;
using Spring.Objects.Factory.Config;

namespace XmlConfig.StringInjection
{
    [TestFixture]
    public class StringInjection
    {
        [Test]
        public void InjectStringFromVariableSource()
        {
            var ctx = new XmlApplicationContext("StringInjection/objects.xml");

            var s = (string) ctx["nameFromStringObjectDefinition"];
            var p1 = (Person)ctx["JohnUsingString.CopyAsFactoryMethod"];
            Assert.AreEqual("John Doe (from string singleton)", s);
            Assert.AreEqual("John Doe (from string singleton)", p1.Name);

            var p2 = (Person)ctx["JohnUsingVariableSource"];
            Assert.AreEqual("John Doe (from string variable source)", p2.Name);

        } 
       
    }
}