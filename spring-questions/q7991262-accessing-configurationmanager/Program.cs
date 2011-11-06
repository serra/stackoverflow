using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using Spring.Context.Support;
using Spring.Objects.Factory.Config;

namespace q7991262
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var ctx = new XmlApplicationContext("objects.xml");
            var fromCode = new MyService
                               {
                                   Connection =
                                       ConfigurationManager.ConnectionStrings["myConnectionName"].ConnectionString
                               };

            Console.WriteLine("{0}: {1}", "From Code: ", fromCode.Connection);

            PrintOutNameAndConnection(ctx);

            Console.ReadLine();
        }

        private static void PrintOutNameAndConnection(XmlApplicationContext ctx)
        {
            foreach (var name in ctx.GetObjectNamesForType(typeof (MyService)))
            {
                var svc = (MyService) ctx.GetObject(name);
                Console.WriteLine("{0} : {1}", name, svc.Connection);
            }
        }
    }

    public class MyService
    {
        public DateTime TheDate { get; set; }
        public string Connection { get; set; }
    }
}
