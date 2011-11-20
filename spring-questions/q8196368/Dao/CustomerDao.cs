using System;
using Aspect.Entities;

namespace Aspect.Dao
{
    public class CustomerDao :ICustomerDao
    {
        #region Implementation of ICustomerDao

        public Customer GetCustomerById(long id)
        {
            return new Customer {Id = id, Name = string.Format("Customer_{0}", id)};
        }

        #endregion
    }
}