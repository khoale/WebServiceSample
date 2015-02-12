namespace WebAPIOData.OData
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http.OData;

    using AdventureWorks.Core;

    using AutoMapper.QueryableExtensions;

    using WebAPIOData.Dtos;

    public class PeopleController : AsyncEntitySetController<PersonDto, int>
    {
        private readonly PersonContext personContext;

        public PeopleController(PersonContext personContext)
        {
            this.personContext = personContext;
        }

        public override async Task<IEnumerable<PersonDto>> Get()
        {
            var people = this.personContext.People.Project().To<PersonDto>();
            var result = await this.QueryOptions.ApplyTo(people).ToListAsync();
            return result.Cast<PersonDto>();
        }
    }
}