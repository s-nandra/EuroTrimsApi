using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EuroTrim.api.Entities;
using Microsoft.EntityFrameworkCore;

namespace EuroTrim.api.Services
{
    public class EuroTrimRepository : IEuroTrimRepository
    {
        private EuroTrimContext _context;

        public EuroTrimRepository(EuroTrimContext context)
        {
            _context= context;
        }

        public bool CustomerExists(Guid customerId)
        {
            return _context.Customers.Any(c => c.Id == customerId);
        }

        public bool ProductExists(Guid productId)
        {
            return _context.Products.Any(p => p.Id == productId);
        }

        public Customer GetCustomer(Guid customerId)
        {
            return _context.Customers.Where(c => c.Id == customerId).FirstOrDefault();
        }

        public void AddCustomer(Customer customer)
        {
            customer.Id = Guid.NewGuid();
            _context.Customers.Add(customer);

            //If any orders attached to customer
            if(customer.Orders.Any())
            {
                foreach (var order in customer.Orders)
                {
                    order.Id = Guid.NewGuid();
                }

            }
        }

        /*public Customer GetCustomer(Guid customerId, bool includeProduct)
        {
            if (includeProduct)
            {
                return _context.Customers.Include(c => c.Product)
                    .Where(c => c.Id == customerId).FirstOrDefault();
            }

            return _context.Customers.Where(c => c.Id == customerId).FirstOrDefault();
        }*/

        public Product GetProductForCustomer(Guid customerId, Guid productId)
        {
            //return _context.Products
            //    .Where(p => p.CustomerId == customerId 
            //        && p.Id==productId).FirstOrDefault();
            throw new Exception();
        }
        
        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.OrderBy(c => c.Name).ToList();
        }

        public IEnumerable<Customer> GetCustomers(IEnumerable<Guid> customerIds)
        {
            return _context.Customers.Where(a => customerIds.Contains(a.Id))
                .OrderBy(a => a.Name)
                .ToList();
        }

        public Product GetProduct(Guid productId)
        {
            return _context.Products.Where(p => p.Id == productId).FirstOrDefault();
        }

        public IEnumerable<Product> GetProductsForCustomer(Guid customerId)
        {

            //return _context.Products
            // .Where(p => p.CustomerId == customerId).ToList();
            throw new Exception();
        }

        public IEnumerable<Order> GetOrdersForCustomer(Guid customerId)
        {
            return _context.Orders
                .Where(o => o.CustomerId == customerId).ToList();
        }

        public Order GetOrderForCustomer(Guid customerId, Guid productId)
        {
            return _context.Orders
                .Where(o => o.CustomerId == customerId
                    && o.ProductId==productId).FirstOrDefault();
        }

        public Order GetOrderForCustomerByOrderId(Guid customerId, Guid orderId)
        {
            return _context.Orders
                .Where(o => o.CustomerId == customerId
                    && o.Id == orderId).FirstOrDefault();
        }




        /*public void AddProductForCustomer(Guid customerId, Product product)
        {
            var customer = GetCustomer(customerId);
            customer.Product.Add(product);
            
        }*/

        public void AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
        }

        public void DeleteCustomer(Customer customer)
        {
            _context.Customers.Remove(customer);
        }

        public void DeleteOrder(Order order)
        {
            _context.Orders.Remove(order);
        }

        public void UpdateProduct(Product product)
        {
            
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void AddOrderForCustomer(Guid customerId,  Order order)
        { 
            var customer = GetCustomer(customerId);

            if (customer != null)
            {
                // if there isn't an id filled out (ie: we're not upserting),
                // we should generate one
                if (order.Id == Guid.Empty)
                {
                    order.Id = Guid.NewGuid();
                    
                }

                customer.Orders.Add(order); 
            }

        }

        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.OrderBy(c => c.ProdName).ToList();

        }
    }
}
