namespace WcfDataServiceToolkitAdvanced.Services
{
    using System.Linq;

    using Microsoft.Data.Services.Toolkit.QueryModel;

    using WcfDataServiceToolkitAdvanced.Dto;
    using WcfDataServiceToolkitAdvanced.Repositories;

    public class PersonODataContext : ODataContext
    {
        private readonly RepositoryFactory repositoryFactory;

        public PersonODataContext(RepositoryFactory repositoryFactory)
        {
            this.repositoryFactory = repositoryFactory;
        }

        public IQueryable<PersonDto> People
        {
            get
            {
                return this.CreateQuery<PersonDto>();
            }
        }

        public override object RepositoryFor(string fullTypeName)
        {
            return this.repositoryFactory.Create(fullTypeName);
        }
    }
}