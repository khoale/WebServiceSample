namespace WcfDataServiceToolkitAdvanced.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using Microsoft.Practices.ServiceLocation;

    public class RepositoryFactory
    {
        private readonly Dictionary<string, Type> serviceTypes;

        public RepositoryFactory()
        {
            this.serviceTypes = new Dictionary<string, Type>();
        }

        public object Create(string fullTypeName)
        {
            Type serviceType;

            if (this.serviceTypes.TryGetValue(fullTypeName, out serviceType))
            {
                return ServiceLocator.Current.GetInstance(serviceType);
            }

            throw new KeyNotFoundException(
                string.Format("Service with name '{0}' does not exist.", fullTypeName));
        }

        public RepositoryRegister<TResource> RegisterResource<TResource>() where TResource : class, new()
        {
            return new RepositoryRegister<TResource>(this.serviceTypes);
        }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed. Suppression is OK here.")]
    public class RepositoryRegister<TResource>
        where TResource : class, new()
    {
        private readonly IDictionary<string, Type> serviceTypes;

        public RepositoryRegister(IDictionary<string, Type> serviceTypes)
        {
            this.serviceTypes = serviceTypes;
        }

        public void As<TRepository>() where TRepository : IRepository<TResource>
        {
            var repositoryType = typeof(TRepository);
            var resourceType = typeof(TResource);

            this.serviceTypes.Add(resourceType.FullName, repositoryType);
        }
    }
}