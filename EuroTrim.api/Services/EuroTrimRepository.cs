using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EuroTrim.api.Entities;
using EuroTrim.api.Helpers;
using EuroTrim.api.Models;
using Microsoft.EntityFrameworkCore;

namespace EuroTrim.api.Services
{
    public class EuroTrimRepository : IEuroTrimRepository
    {
        private EuroTrimContext _context;
        private IPropertyMappingService _propertyMappingService;

        public EuroTrimRepository(EuroTrimContext context,
            IPropertyMappingService propertyMappingService)
        {
            _context= context;
            _propertyMappingService = propertyMappingService;
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
        
        public PagedList<Customer> GetCustomers(CustomersResourceParameters customersResourceParameters)
        {
            //var collectionBeforePaging = _context.Customers
            //    .OrderBy(c => c.Name).AsQueryable();

            var collectionBeforePaging =
                _context.Customers.ApplySort(customersResourceParameters.OrderBy,
                _propertyMappingService.GetPropertyMapping<CustomerDto, Customer>());

            if (!string.IsNullOrEmpty(customersResourceParameters.City))
            {
                var cityForWhereClause = customersResourceParameters.City
                    .Trim().ToLowerInvariant();

                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.City != null && a.City.ToLowerInvariant() == cityForWhereClause);
            }

            if(!string.IsNullOrEmpty(customersResourceParameters.SearchQuery))
            {
                var searchorWhereClause = customersResourceParameters.SearchQuery
                   .Trim().ToLowerInvariant();

                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.Name.ToLowerInvariant().Contains(searchorWhereClause)
                    || a.PostCode.ToLowerInvariant().Contains(searchorWhereClause)
                    || a.Address1 != null && a.Address1.ToLowerInvariant().Contains(searchorWhereClause)
                    || a.Address2 != null && a.Address2.ToLowerInvariant().Contains(searchorWhereClause)
                    );
            }

            return PagedList<Customer>.Create(collectionBeforePaging,
                customersResourceParameters.PageNumber,
                customersResourceParameters.PageSize);
 
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
//            var query1 = _context.Orders   // your starting point
//.Join(_context.Products, // the source table of the inner join
//o => o.ProductId,        // Select the primary key (the first part of the "on" clause in an sql "join" statement)
//p => p.Id,   // Select the foreign key (the second part of the "on" clause)
//(o, p) => new { O = o, P = p }) // selection
//.Where(postAndMeta => postAndMeta.O.ProductId == productId);



            var query = (from orders in _context.Orders
                         join products in _context.Products on orders.ProductId equals products.Id
                         select orders
                         ).FirstOrDefault();

            return query;
            /*
            return _context.Orders
                .Where(o => o.CustomerId == customerId
                    && o.ProductId==productId).FirstOrDefault();
            */
        }

        //public Product GetProductsByOrder(Order _order)
        //{

        //    var query = (from orders in _context.Orders
        //                 join products in _context.Products on orders.ProductId equals products.Id
        //                 select products
        //     ).FirstOrDefault();

        //    return query;

        //}

        public IEnumerable<OrderDto> GetOrderByCustomerId(Guid customerId)
        {

            var query = (from orders in _context.Orders
                         join products in _context.Products on orders.ProductId equals products.Id
                         where orders.CustomerId == customerId
                         select new OrderDto {
                            Id=orders.Id,
                            DateOrderCreated=orders.DateOrderCreated,
                            CustomerId=orders.CustomerId,
                            ProductId =orders.ProductId,
                            Products=products
                         }  
             );

            return query.ToList();

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

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.OrderBy(c => c.Name).ToList();
        }

        public User GetUser(string username, string password)
        {
            return _context.Users.Where(c => c.Username == username && c.Password==password).FirstOrDefault();
        }
    }
}
