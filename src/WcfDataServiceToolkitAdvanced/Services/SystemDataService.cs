namespace WcfDataServiceToolkitAdvanced.Services
{
    using Autofac;

    public class SystemDataService : BaseODataService<SystemODataContext>
    {
        public SystemDataService(ILifetimeScope lifetimeScope)
            : base(lifetimeScope)
        {
        }
    }
}