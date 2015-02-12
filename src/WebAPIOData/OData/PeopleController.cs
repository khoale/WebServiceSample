namespace WebAPIOData.OData
{
    using System.Linq;
    using System.Web.OData;

    using AdventureWorks.Core;

    using AutoMapper.QueryableExtensions;

    using WebAPIOData.Dtos;

    public class PeopleController : ODataController
    {
        private readonly PersonContext personContext;

        public PeopleController(PersonContext personContext)
        {
            this.personContext = personContext;
        }

        [EnableQuery]
        public IQueryable<PersonDto> Get()
        {
            return this.personContext.People.Project().To<PersonDto>();
        }
    }
}