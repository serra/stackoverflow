using System;
using Aspect.Dao;
using Aspect.Entities;

namespace Aspect.Managers
{
    public interface IDbCustomerManager
    {
        Customer GetCustomerById(long id);
    }

    public class DbCustomerManager:IDbCustomerManager
    {
        private ICustomerDao CustomerDao { get; set; }

        #region Implementation of IDbCustomerManager

        public Customer GetCustomerById(long id)
        {
            throw new DbException(string.Format("Problem load customer with Id: {0}",id));
            //return new Customer { Id = id, Name = string.Format("Customer_{0}", id) };
            //return CustomerDao.GetCustomerById(id);
        }

        #endregion
    }
}