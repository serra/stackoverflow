using System;
using System.Collections.Generic;
using NUnit.Framework;
using Spring.Context.Support;

namespace q8167718
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new XmlApplicationContext("objects.xml");
            var customerDao = (ICustomerDao)context["customerDao"];
            
            Console.ReadLine();
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Main()
        {
            var context = new XmlApplicationContext("objects.xml");
            var customerDao = (ICustomerDao)context["customerDao"];
            
            try
            {
                customerDao.Save(new Customer { Id = 1, Name = "Customer_1" });
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Something", ex.Message);
                Assert.Pass("This exception is expected");
            }
            catch (Exception ex)
            {
                Assert.Fail(string.Format("Unexpected exception of type: {0} \n\n Exception message: {1}",
                    ex.GetType(), ex.Message));
            }
        }
    }
    
    #region classes

    public class Customer
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public interface ICustomerDao
    {
        void Save(Customer customer);
    }

    public class CustomerDao : ICustomerDao
    {
        #region Implementation of ICustomerDao

        public void Save(Customer customer)
        {
            throw new CustomerException(
                string.Format("Customer with id {0} already exist in repository", customer.Id));
        }

        #endregion
    }

    public class CustomerException : Exception
    {
        public CustomerException(string msg)
            : base(msg)
        {

        }
    }

    #endregion
}
