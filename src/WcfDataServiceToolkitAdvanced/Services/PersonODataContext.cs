namespace WcfDataServiceToolkitAdvanced.Services
{
    using System.Linq;

    using AdventureWorks.Core;

    using Microsoft.Data.Services.Toolkit.QueryModel;

    using WcfDataServiceToolkitAdvanced.Repositories;

    public class PersonODataContext : ODataContext
    {
        private readonly RepositoryFactory repositoryFactory;

        public PersonODataContext(RepositoryFactory repositoryFactory)
        {
            this.repositoryFactory = repositoryFactory;
        }

        public IQueryable<Person> People
        {
            get
            {
                return this.CreateQuery<Person>();
            }
        }

        public override object RepositoryFor(string fullTypeName)
        {
            return this.repositoryFactory.Create(fullTypeName);
        }
    }
}