using System;
using AopAlliance.Intercept;
using NUnit.Framework;
using Spring.Context.Support;

namespace Aop.q11630224
{
    [TestFixture]
    public class InterceptionTests
    {
        [Test]
        public void Main()
        {
            var ctx = new XmlApplicationContext("q11630224/objects.xml");
            var report1 = (IReport)ctx.GetObject("report1");
            var report2 = (IReport)ctx.GetObject("report1");
            Console.WriteLine(UsageTrackingInterceptor.UsageCount);
            report1.WriteReport();
            report2.WriteReport();
            Console.WriteLine(UsageTrackingInterceptor.UsageCount);
        }
    }

    public class Report : IReport
    {
        public void WriteReport()
        {
            Console.WriteLine("Writing report ... Done!");
        }
    }

    public interface IReport
    {
        void WriteReport();
    }

    public class UsageTrackingInterceptor : IMethodInterceptor
    {
        public static int UsageCount { get; private set; }

        public object Invoke(IMethodInvocation invocation)
        {
            UsageCount++;
            return invocation.Proceed();
        }
    } 
}
