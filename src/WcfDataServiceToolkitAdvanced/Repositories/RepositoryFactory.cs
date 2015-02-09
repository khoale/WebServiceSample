﻿namespace WcfDataServiceToolkitAdvanced.Repositories
{
    using System;
    using System.Collections.Generic;

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

        public void RegisterResource<TResource, TRepository>() 
            where TResource : class, new()
            where TRepository : IRepository<TResource>
        {
            var repositoryType = typeof(TRepository);
            var resourceType = typeof(TResource);

            serviceTypes.Add(resourceType.FullName, repositoryType);
        }
    }
}