using EuroTrim.api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EuroTrim.api.Services
{
    public interface IEuroTrimRepository
    {
        IEnumerable<Customer> GetCustomers();

        Customer GetCustomer(int customerId,bool includeProduct);

        IEnumerable<Product> GetProductForCustomer(int customerId);

        Product GetProduct(int productId);

        Product GetCustomerProduct(int customerId, int productId);

    }
}
