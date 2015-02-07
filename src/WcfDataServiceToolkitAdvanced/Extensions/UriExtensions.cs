namespace WcfDataServiceToolkitAdvanced.Extensions
{
    using System.Collections.Generic;
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
    }
}