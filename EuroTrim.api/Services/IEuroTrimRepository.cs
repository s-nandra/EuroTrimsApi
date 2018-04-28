using EuroTrim.api.Entities;
using EuroTrim.api.Helpers;
using System;
using System.Collections.Generic;
 

namespace EuroTrim.api.Services
{
    public interface IEuroTrimRepository
    {
        bool CustomerExists(Guid customerId);
        bool ProductExists(Guid productId);

        Customer GetCustomer(Guid id);
        //Customer GetCustomer(Guid customerId, bool includeProduct);
        PagedList<Customer> GetCustomers(CustomersResourceParameters customersResourceParameters);

        IEnumerable<Customer> GetCustomers(IEnumerable<Guid> customerIds);

        IEnumerable<Order> GetOrdersForCustomer(Guid customerId);




        Order GetOrderForCustomer(Guid customerId, Guid productId);

        Order GetOrderForCustomerByOrderId(Guid customerId, Guid orderId);

        void AddCustomer(Customer customerEntity);

        void AddOrderForCustomer(Guid customerId,  Order order);


        IEnumerable<Product> GetProductsForCustomer(Guid customerId);



        IEnumerable<Product> GetProducts();

        Product GetProduct(Guid productId);

        Product GetProductForCustomer(Guid customerId, Guid productId);
   

        //void AddProductForCustomer(Guid customerId, Product product);

        void AddProduct(Product product);
        void DeleteProduct(Product product);
        void DeleteOrder(Order ordersForCustomerFromRepo);

        void DeleteCustomer(Customer customerFromRepo);

        void UpdateProduct(Product productToUpdate);

        bool Save();
        
    }
}
