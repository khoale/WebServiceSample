namespace WcfDataServiceToolkitAdvanced.Services
{
    using System.Data.Services;
    using System.Data.Services.Common;
    using System.ServiceModel;

    using Autofac;

    using Microsoft.Data.Services.Toolkit;

    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class PersonDataService : ODataService<PersonODataContext>
    {
        private readonly ILifetimeScope lifetimeScope;

        public PersonDataService(ILifetimeScope lifetimeScope)
        {
            this.lifetimeScope = lifetimeScope;
        }

        // ReSharper disable once UnusedMember.Global
        public static void InitializeService(DataServiceConfiguration config)
        {
            // This is required
            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
            config.UseVerboseErrors = true;
        }

        protected override PersonODataContext CreateDataSource()
        {
            return this.lifetimeScope.Resolve<PersonODataContext>();
        }
    }
}