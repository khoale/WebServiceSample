namespace WcfDataServiceToolkitAdvanced.Services
{
    using System.ServiceModel;

    using Autofac;

    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class PersonDataService : BaseODataService<PersonODataContext>
    {
        public PersonDataService(ILifetimeScope lifetimeScope)
            : base(lifetimeScope)
        {
        }
    }
}