namespace WcfDataServiceToolkitAdvanced
{
    using System;
    using System.Collections.Generic;
    using System.Data.Services;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Description;

    using Autofac;
    using Autofac.Integration.Wcf;

    using WcfDataServiceToolkitAdvanced.Extensions;

    public class ODataServiceHostFactory : AutofacServiceHostFactory
    {
        #region Overrides of AutofacHostFactory

        /// <summary>
        /// Creates a <see cref="T:System.ServiceModel.ServiceHost"/> for a specified type of service with a specific base address.
        /// </summary>
        /// <param name="serviceType">Specifies the type of service to host.</param>
        /// <param name="baseAddresses">The <see cref="T:System.Array"/> of type <see cref="T:System.Uri"/> that contains the base addresses for the service hosted.</param>
        /// <returns>
        /// A <see cref="T:System.ServiceModel.ServiceHost"/> for the type of service specified with a specific base address.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope",
            Justification = "Host lifetime will be handled by Autofac.")]
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }

            if (baseAddresses == null)
            {
                throw new ArgumentNullException("baseAddresses");
            }

            var host = new DataServiceHost(serviceType, baseAddresses);
            var interfaces = serviceType.GetInterfaces()
                .Where(i => i.GetCustomAttributes(typeof(ServiceContractAttribute), false).Any())
                .Where(i => i != typeof(IRequestHandler));
            var binding = new WebHttpBinding
            {
                MaxReceivedMessageSize = 65536 * 8,
                MaxBufferSize = 65536 * 8,
                ReaderQuotas =
                {
                    MaxArrayLength = 16384 * 8
                }
            };

            // Create service endpoint for non-OData service and set service behaviour.
            // NOTE: MUST register WCF services before OData services.
            foreach (var @interface in interfaces)
            {
                host.AddServiceEndpoint(@interface, binding, @interface.Name.Substring(1))
                    .ConfigureWebHttpBehavior();
            }

            // Do not enable help page on OData services.
            host.AddServiceEndpoint(typeof(IRequestHandler), binding, string.Empty)
                .ConfigureWebHttpBehavior(false);

            // Fix api not found issue on .NET 4.5 when use SSL
            // The System.ServiceModel.Web.WebServiceHost object no longer adds 
            // a default endpoint if an explicit endpoint has been added by application code.
            // http://msdn.microsoft.com/en-us/library/hh367887(v=VS.110).aspx#wcf
            if (baseAddresses.SupportHttpsBinding())
            {
                var httpsBinding = new WebHttpBinding(WebHttpSecurityMode.Transport)
                {
                    MaxReceivedMessageSize = 65536 * 8,
                    MaxBufferSize = 65536 * 8,
                    ReaderQuotas =
                    {
                        MaxArrayLength = 16384 * 8
                    }
                };

                host.AddServiceEndpoint(typeof(IRequestHandler), httpsBinding, string.Empty)
                    .ConfigureWebHttpBehavior(false);
            }
            
            var behaviors = Container.Resolve<IEnumerable<IServiceBehavior>>();
            host.AddBehaviors(behaviors);

            return host;
        }

        /// <summary>
        /// Creates a <see cref="T:System.ServiceModel.ServiceHost"/> for a specified type of service with a specific base address.
        /// </summary>
        /// <param name="singletonInstance">Specifies the singleton service instance to host.</param>
        /// <param name="baseAddresses">The <see cref="T:System.Array"/> of type <see cref="T:System.Uri"/> that contains the base addresses for the service hosted.</param>
        /// <returns>
        /// A <see cref="T:System.ServiceModel.ServiceHost"/> for the singleton service instance specified with a specific base address.
        /// </returns>
        protected override ServiceHost CreateSingletonServiceHost(object singletonInstance, Uri[] baseAddresses)
        {
            throw new NotImplementedException("The DataServiceHost cannot create as singleton.");
        }

        #endregion Overrides of AutofacHostFactory
    }
}