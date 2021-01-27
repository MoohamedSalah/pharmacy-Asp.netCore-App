using System.Collections.Generic;

namespace WebApplication2Core.Models
{
    public interface IProductReposetory
    {
        Product GetProduct(int Id);
        IEnumerable<Product> GetProducts();
        Product create(Product product);
        Product Update(Product productChenge);
        Product Delete(int Id);
    }
}
