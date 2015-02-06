namespace WcfDataServiceToolkitAdvanced.Extensions
{
    using System.ServiceModel.Description;
    using System.ServiceModel.Web;

    public static class ServiceEndpointExtensions
    {
        /// <summary>
        /// Sets DefaultOutgoingRequestFormat/DefaultOutgoingResponseFormat to
        /// WebMessageFormat.Json and optionally configure help page for given enpoint.
        /// </summary>
        /// <param name="endpoint">The endpoint to configure.</param>
        /// <param name="helpEnable">true to enable Help page; false to disable.</param>
        /// <returns>The configured endpoint.</returns>
        public static ServiceEndpoint ConfigureWebHttpBehavior(this ServiceEndpoint endpoint, bool helpEnable = true)
        {
            var serviceBehavior = new WebHttpBehavior
            {
                HelpEnabled = helpEnable,
                DefaultOutgoingRequestFormat = WebMessageFormat.Json,
                DefaultOutgoingResponseFormat = WebMessageFormat.Json,
                FaultExceptionEnabled = true // This prevent WebErrorHandler add to channelDispatcher.ErrorHandlers automatically.
            };

            endpoint.Behaviors.Add(serviceBehavior);

            return endpoint;
        }
    }
}