namespace WcfDataServiceToolkitAdvanced.Services
{
    using System.Linq;

    using WcfDataServiceToolkitAdvanced.Dto;
    using WcfDataServiceToolkitAdvanced.Repositories;

    public class SystemODataContext : BaseODataContext
    {
        public SystemODataContext(RepositoryFactory repositoryFactory)
            : base(repositoryFactory)
        {
        }

        public IQueryable<ExceptionDto> Exceptions
        {
            get
            {
                return this.CreateQuery<ExceptionDto>();
            }
        }
    }
}