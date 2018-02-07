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

        public Customer GetCustomer(int customerId, bool includeProduct)
        {
            if (includeProduct)
            {
                return _context.Customers.Include(c => c.Product)
                    .Where(c => c.Id == customerId).FirstOrDefault();
            }

            return _context.Customers.Where(c => c.Id == customerId).FirstOrDefault();
        }

        public Product GetCustomerProduct(int customerId, int productId)
        {
            throw new NotImplementedException();
        }
        
        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.OrderBy(c => c.Name).ToList();
        }

        public Product GetProduct(int productId)
        {
            return _context.Products.Where(p => p.Id == productId).FirstOrDefault();
        }

        public IEnumerable<Product> GetProductForCustomer(int customerId)
        {
            return _context.Products
             .Where(p => p.CustomerId == customerId).ToList();
        }
    }
}
