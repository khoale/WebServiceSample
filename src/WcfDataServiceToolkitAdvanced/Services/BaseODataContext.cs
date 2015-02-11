namespace WcfDataServiceToolkitAdvanced.Services
{
    using Microsoft.Data.Services.Toolkit.QueryModel;

    using WcfDataServiceToolkitAdvanced.Repositories;

    public abstract class BaseODataContext : ODataContext
    {
        private readonly RepositoryFactory repositoryFactory;

        public BaseODataContext(RepositoryFactory repositoryFactory)
        {
            this.repositoryFactory = repositoryFactory;
        }

        public override object RepositoryFor(string fullTypeName)
        {
            return this.repositoryFactory.Create(fullTypeName);
        }
    }
}