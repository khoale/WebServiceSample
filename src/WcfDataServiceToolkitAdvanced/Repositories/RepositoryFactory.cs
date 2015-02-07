namespace WcfDataServiceToolkitAdvanced.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using Autofac;

    public class RepositoryFactory
    {
        private static readonly Dictionary<string, Type> serviceTypes;

        private readonly ILifetimeScope lifetimeScope;

        static RepositoryFactory()
        {
            serviceTypes = new Dictionary<string, Type>();
        }

        public RepositoryFactory(ILifetimeScope lifetimeScope)
        {
            // Careful when using lifetime scope with single instance
            this.lifetimeScope = lifetimeScope;
        }

        public object Create(string fullTypeName)
        {
            Type serviceType;

            if (serviceTypes.TryGetValue(fullTypeName, out serviceType))
            {
                return this.lifetimeScope.Resolve(serviceType);
            }

            throw new KeyNotFoundException(
                string.Format("Service with name '{0}' does not exist.", fullTypeName));
        }

        public RepositoryRegister<TResource> RegisterResource<TResource>() where TResource : class, new()
        {
            return new RepositoryRegister<TResource>(serviceTypes);
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