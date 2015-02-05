namespace WcfDataService.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using WcfDataService.Dtos;

    public class ProductRepository : RepositoryBase<Product, string>
    {
        private static readonly List<Product> Products;

        static ProductRepository()
        {
            Products = new List<Product>();
            Products.Add(new Product { SkuId = "Nex9", Name = "Google Nexus 9" });
            Products.Add(new Product { SkuId = "Sur", Name = "Surface Pro" });
        }

        public override IQueryable<Product> GetAll()
        {
            return Products.AsQueryable();
        }
    }
}