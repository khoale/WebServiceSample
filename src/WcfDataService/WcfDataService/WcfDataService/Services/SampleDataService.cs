namespace WcfDataService.Services
{
    using System.Data.Services;
    using System.Data.Services.Common;
    using System.ServiceModel;

    using Microsoft.Data.Services.Toolkit;

    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class SampleDataService : ODataService<SampleDataContext>
    {
        public static void InitializeService(DataServiceConfiguration config)
        {
            // This is required
            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
            config.UseVerboseErrors = true;
        }
    }
}