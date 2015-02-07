namespace WcfDataServiceToolkitAdvanced.Repositories
{
    using System.Linq;

    using AdventureWorks.Core;

    using AutoMapper.QueryableExtensions;

    using WcfDataServiceToolkitAdvanced.Dto;

    public class PeopleRepository : RepositoryBase<PersonDto, int>
    {
        private readonly PersonContext personContext;

        public PeopleRepository(PersonContext personContext)
        {
            this.personContext = personContext;
        }

        public override IQueryable<PersonDto> GetAll()
        {
            return this.personContext.People.Project().To<PersonDto>();
        }
    }
}