using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using NUnit.Framework;
using Spring.Core;
using Spring.Expressions;

namespace Expressions
{
    internal class Named
    {
        public string Name { get; set; }
    }

    [TestFixture]
    public class Q10385572ExpandoObject
    {
        [Test]
        public void SimpleObjectWithNameTest()
        {
            var n = new Named() {Name = "Adam"};
            Assert.AreEqual("Adam", (string)ExpressionEvaluator.GetValue(n, "Name"));
        }

        [Test]
        public void DynamicObjectTest()
        {
            dynamic named = new ExpandoObject();
            named.Name = "Adam";
            Assert.AreEqual("Adam", named.Name);
        }

        [Test]
        public void DynamicObjectInExpressionTest()
        {
            dynamic named = new ExpandoObject();
            named.Name = "Adam";
            Assert.Throws<InvalidPropertyException>(() => { var s = (string)ExpressionEvaluator.GetValue(named, "Name"); });
        }
        
        [Test]
        public void DynamicObjectAsDictionaryInExpressionTest()
        {
            dynamic named = new ExpandoObject();
            named.Name = "Adam";
            Assert.Throws<InvalidPropertyException>(() => { var s = (string)ExpressionEvaluator.GetValue(named, "Name"); });

            var dic2 = new Dictionary<string, string>();
            dic2.Add("a", "b");
            Assert.AreEqual("b", ExpressionEvaluator.GetValue(dic2, "#root['a']"));

            var dic = new Dictionary<string, object>(named);
            Assert.AreEqual("Adam", dic["Name"]);
            Assert.AreEqual("Adam", ExpressionEvaluator.GetValue(dic, "['Name']"));    
            Assert.AreEqual("Adam", ExpressionEvaluator.GetValue(dic, "#root['Name']"));

            var ht = new System.Collections.Hashtable(named);

            //Assert.AreEqual("Adam", ExpressionEvaluator.GetValue(dic, "new System.Collections.Hashtable(#root)['Name']"));
            Assert.AreEqual("Adam", ExpressionEvaluator.GetValue(ht, "['Name']"));
            Assert.AreEqual("Adam", ExpressionEvaluator.GetValue(named, "new System.Collections.Hashtable(#root)['Name']"));
        }
    }
}