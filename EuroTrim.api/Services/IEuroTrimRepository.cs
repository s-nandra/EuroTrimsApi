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

        bool ProductExists(int productId);

        IEnumerable<Customer> GetCustomers();

        Customer GetCustomer(int customerId,bool includeProduct);

        IEnumerable<Product> GetProductsForCustomer(int customerId);

        Product GetProduct(int productId);

        Product GetProductForCustomer(int customerId, int productId);

        void AddProductForCustomer(int customerId, Product product);

        void AddProduct(Product product);
        void DeleteProduct(Product product);


        bool Save();
    }
}
