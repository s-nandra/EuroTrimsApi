using EuroTrim.api.Entities;
using EuroTrim.api.Helpers;
using EuroTrim.api.Models;
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
        IEnumerable<DiscountBand> GetDiscountBands();
        Order GetOrderForCustomer(Guid customerId, Guid productId);

        Order GetOrderForCustomerByOrderId(Guid customerId, Guid orderId);

        CustomerProductAllocation GetCustomerProductAllocationByCustomerIdandProductId(Guid customerId, Guid productId);

        
        CustomerProductAllocation GetCustomerProductAllocationById(Guid id);
        
        IEnumerable<User> GetUsers();

        User GetUser(string username, string password);

        void AddCustomer(Customer customerEntity);

        void AddCustomerDiscountAllocation(CustomerProductAllocation cpa);
        
        void UpdateCustomerDiscountAllocation(CustomerProductAllocation cpa);

        void AddOrderForCustomer(Guid customerId,  Order order);


        IEnumerable<Product> GetProductsForCustomer(Guid customerId);
        IEnumerable<Product> GetProducts();

        Product GetProduct(Guid productId);

        Product GetProductForCustomer(Guid customerId, Guid productId);
   

        //void AddProductForCustomer(Guid customerId, Product product);

        void AddProduct(Product product);
        IEnumerable<ProductDto> GetProductsWithBands();
        void DeleteProduct(Product product);
        void DeleteOrder(Order ordersForCustomerFromRepo);

        void DeleteCustomer(Customer customerFromRepo);

        void UpdateProduct(Product productToUpdate);

        bool Save();
        IEnumerable<OrderDto> GetOrderByCustomerId(Guid customerId);

        IEnumerable<CustomerProductAllocationDto> GetCustomerProductAllocationByCustomerId(Guid customerId);
    }
}
