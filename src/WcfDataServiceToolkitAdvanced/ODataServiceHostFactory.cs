namespace WcfDataServiceToolkitAdvanced
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.ServiceModel.Description;

    using Autofac;
    using Autofac.Integration.Wcf;

    using WcfDataServiceToolkitAdvanced.Extensions;

    public class ODataServiceHostFactory : AutofacWebServiceHostFactory
    {
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

            var host = base.CreateServiceHost(serviceType, baseAddresses);

            // Auto register service behaviors
            var behaviors = Container.Resolve<IEnumerable<IServiceBehavior>>();
            host.AddBehaviors(behaviors);

            return host;
        }
    }
}