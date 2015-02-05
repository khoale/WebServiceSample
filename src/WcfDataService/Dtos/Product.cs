namespace WcfDataService.Dtos
{
    using System.Data.Services.Common;

    [DataServiceKey("SkuId")]
    public class Product
    {
        public string SkuId { get; set; }

        public string Name { get; set; }
    }
}