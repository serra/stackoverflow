using Aspect.Entities;

namespace Aspect.Dao
{
    public interface ICustomerDao
    {
        Customer GetCustomerById(long id);
    }
}