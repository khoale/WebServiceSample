namespace WebAPIOData.OData
{
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.OData;

    using AdventureWorks.Core;

    using AutoMapper.QueryableExtensions;

    using WebAPIOData.Dtos;

    public class CountryRegionsController : ODataController
    {
        private readonly PersonContext personContext;

        public CountryRegionsController(PersonContext personContext)
        {
            this.personContext = personContext;
        }

        [EnableQuery]
        public IQueryable<CountryRegionDto> Get()
        {
            return this.personContext.CountryRegions.Project().To<CountryRegionDto>();
        }

        [HttpGet]
        public IHttpActionResult TotalState(string countryRegionCode)
        {
            var count = this.personContext.StateProvinces
                .Count(x => x.CountryRegionCode == countryRegionCode);

            return this.Ok(count);
        }

        [HttpGet]
        public IHttpActionResult Total()
        {
            var count = this.personContext.StateProvinces.Count();
            return this.Ok(count);
        }
    }
}