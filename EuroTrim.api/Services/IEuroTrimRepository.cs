using EuroTrim.api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EuroTrim.api.Services
{
    public interface IEuroTrimRepository
    {
        bool CustomerExists(int customerId);

        IEnumerable<Customer> GetCustomers();

        Customer GetCustomer(int customerId,bool includeProduct);

        IEnumerable<Product> GetProductsForCustomer(int customerId);

        Product GetProduct(int productId);

        Product GetProductForCustomer(int customerId, int productId);
   
    }
}
