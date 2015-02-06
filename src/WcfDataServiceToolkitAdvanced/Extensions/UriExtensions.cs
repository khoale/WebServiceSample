namespace WcfDataServiceToolkitAdvanced.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Description;

    public static class UriExtensions
    {
        public static ServiceHostBase AddBehaviors(
            this ServiceHostBase serviceHostBase, IEnumerable<IServiceBehavior> behaviors)
        {
            foreach (var behavior in behaviors)
            {
                serviceHostBase.Description.Behaviors.Add(behavior);
            }

            return serviceHostBase;
        }

        public static bool SupportHttpsBinding(this Uri[] baseAddresses)
        {
            return baseAddresses.Any(x => x.Scheme.Equals("https", StringComparison.InvariantCultureIgnoreCase));
        } 
    }
}