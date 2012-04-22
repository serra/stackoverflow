using System;
using NUnit.Framework;
using Spring.Core.TypeResolution;
using Spring.Expressions;

namespace Expressions
{
    [TestFixture]
    public class Q10150013_EvaluateProperty
    {
        [Test]
        public void First_test()
        {
            TypeRegistry.RegisterType("IMailMessage", typeof(IMailMessage));

            var mm = (IMailMessage) new MailMessage();
            mm.Subject = "abc vijay def";
            // does not work; spel thinks it's a MailMessage
            //var val = ExpressionEvaluator.GetValue(mm, "(#root as T(IMailMessage)).Subject");
            var arr = new IMailMessage[] {mm};
            var val = ExpressionEvaluator.GetValue(arr, "(convert(IMailMessage))[0].Subject");
            Console.WriteLine(val);
            //Assert.True(val);
        }
    }

    public interface IMailMessage
    {
        string To { get; set; }
        string From { get; set; }
        string Subject { get; set; }
        string Message { get; set; }
    }

    public class MailMessage :IMailMessage
    {
        string IMailMessage.To { get; set; }
        string IMailMessage.From { get; set; }
        string IMailMessage.Subject { get; set; }
        string IMailMessage.Message { get; set; }
    }
}