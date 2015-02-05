namespace WcfDataServiceEF.Service
{
    using System.Data.Services;
    using System.Data.Services.Common;
    using System.Data.Services.Providers;
    using System.ServiceModel;

    using WcfDataServiceEF.Data;

    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class PersonService : EntityFrameworkDataService<PersonContext>
    {
        public static void InitializeService(DataServiceConfiguration config)
        {
            // This is required
            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
            config.UseVerboseErrors = true;
        }
    }
}