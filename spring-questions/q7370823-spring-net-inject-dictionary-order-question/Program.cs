using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Context.Support;

namespace q7370823_spring_net_inject_dictionary_order_question
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new XmlApplicationContext("objects.xml");
            var dic = (IDictionary<string, string>) ctx["dictLang"];
            foreach (var kv in dic)
            {
                Console.WriteLine("{0} {1}", kv.Key, kv.Value);
            }
            Console.ReadLine();
        }
    }
}
