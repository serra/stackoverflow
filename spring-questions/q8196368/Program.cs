using System;
using System.Reflection;
using Aspect.Entities;
using Aspect.Managers;
using Common.Logging;
using Spring.Context.Support;

namespace Aspect
{
    class Program
    {
        private static void ShowCustomerData(Customer customer)
        {
            Console.WriteLine("Name: {0}",customer.Name);
        }

        static void Main(string[] args)
        {
            //ILog log = LogManager.GetLogger(Assembly.GetExecutingAssembly().GetType());
            //log.Info("info msg");

            //try
            //{
                var springContext = ContextRegistry.GetContext();
                var dbMgr = (IDbCustomerManager)springContext["theDbCustomerManager"];
                dbMgr.GetCustomerById(1);
            //}
            //catch (Exception ex)
            //{
                
            //    Console.WriteLine("{0}\n{1}",ex.GetType(),ex.Message);
            //}


            Console.WriteLine("done!");
            Console.ReadKey();
        }
    }
}
