namespace WebAPIOData.OData
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.OData;

    using AdventureWorks.Core;

    using AutoMapper.QueryableExtensions;

    using WebAPIOData.Dtos;

    public class CountryRegionsController : AsyncEntitySetController<CountryRegionDto, string>
    {
        private readonly PersonContext personContext;

        public CountryRegionsController(PersonContext personContext)
        {
            this.personContext = personContext;
        }

        public override async Task<IEnumerable<CountryRegionDto>> Get()
        {
            var countryRegions = this.personContext.CountryRegions.Project().To<CountryRegionDto>();
            var result = await this.QueryOptions.ApplyTo(countryRegions).ToListAsync();
            return result.Cast<CountryRegionDto>();
        }

        [HttpGet]
        public IHttpActionResult TotalState(string countryRegionCode)
        {
            var count = this.personContext
                .StateProvinces.Count(x => x.CountryRegionCode == countryRegionCode);
            return this.Ok(count);
        }
    }
}