using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebApplication2Core.Models
{
    public class SQLProductReposetory : IProductReposetory
    {
        private readonly AppDbContext _context;

        public SQLProductReposetory(AppDbContext context)
        {
            _context = context;
        }
        public Product create(Product product)
        {
            _context.products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public Product Delete(int Id)
        {
            var product = _context.products.Find(Id);
            if (product != null)
            {
                product.IsActive = false;
                var p = _context.products.Attach(product);
                p.State = EntityState.Modified;
                _context.SaveChanges();
            }
            return product;
        }

        public Product GetProduct(int Id)
        {
            return _context.products.Find(Id);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _context.products;
        }

        public Product Update(Product productChenge)
        {
            var product = _context.products.Attach(productChenge);
            product.State = EntityState.Modified;
            _context.SaveChanges();
            return productChenge;

        }

    }
}
