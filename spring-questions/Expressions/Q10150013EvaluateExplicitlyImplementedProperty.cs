using System;
using NUnit.Framework;
using Spring.Core.TypeResolution;
using Spring.Expressions;

namespace Expressions
{
    [TestFixture]
    public class Q10150013EvaluateExplicitlyImplementedProperty
    {
        [Test, Ignore]
        public void First_test()
        {
            var mm = (IMailMessage) new MailMessage();
            mm.Subject = "abc vijay def";
            var val = (bool)ExpressionEvaluator.GetValue(mm, "'IMailMessage.Subject'.Contains('vijay')");
            Console.WriteLine(val);
            Assert.True(val);
        }
    }

    public interface IMailMessage
    {
        string Subject { get; set; }
    }

    public class MailMessage : IMailMessage
    {
        string IMailMessage.Subject { get; set; }
    }
}