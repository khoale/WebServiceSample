namespace WebAPIOData.Dtos
{
    using System.ComponentModel.DataAnnotations;

    public class CountryRegionDto
    {
        [Key]
        public string CountryRegionCode { get; set; }

        public string Name { get; set; }
    }
}