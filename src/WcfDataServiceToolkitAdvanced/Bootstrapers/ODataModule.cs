namespace WcfDataServiceToolkitAdvanced.Bootstrapers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.ServiceModel.Activation;
    using System.Web.Routing;

    using AdventureWorks.Core;

    using Autofac;
    using Autofac.Integration.Wcf;

    using Microsoft.Data.Services.Toolkit;

    using WcfDataServiceToolkitAdvanced.Repositories;
    using WcfDataServiceToolkitAdvanced.Services;

    public class ODataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RepositoryFactory>().SingleInstance();
            builder.RegisterType<ODataServiceHostFactory>().SingleInstance();
            builder.RegisterType<ODataStartable>().As<IStartable>();
        }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed. Suppression is OK here.")]
    public class ODataStartable : IStartable
    {
        private readonly IComponentContext componentContext;

        private readonly ODataServiceHostFactory serviceHostFactory;

        private readonly RepositoryFactory repositoryFactory;

        public ODataStartable(IComponentContext componentContext, ODataServiceHostFactory serviceHostFactory, RepositoryFactory repositoryFactory)
        {
            this.componentContext = componentContext;
            this.serviceHostFactory = serviceHostFactory;
            this.repositoryFactory = repositoryFactory;
        }

        public void Start()
        {
            AutofacHostFactory.Container = this.componentContext.Resolve<ILifetimeScope>();
            this.RegisterRouteTable();
            this.RegisterRepositories();
        }

        private void RegisterRepositories()
        {
            this.repositoryFactory
                .RegisterResource<Person>().As<PeopleRepository>();
        }

        private void RegisterRouteTable()
        {
            var serviceTypes = this.GetAllServiceTypes();
            foreach (var serviceType in serviceTypes)
            {
                var serviceName = this.GetServiceName(serviceType);
                var prefix = string.Format("api/{0}", serviceName).ToLower();
                RouteTable.Routes.Add(new ServiceRoute(prefix, this.serviceHostFactory, serviceType));
            }
        }

        private IEnumerable<Type> GetAllServiceTypes()
        {
            return typeof(PersonDataService).Assembly.GetTypes()
                .Where(x => x.BaseType != null && x.BaseType.IsGenericType && x.BaseType.GetGenericTypeDefinition() == typeof(ODataService<>))
                .ToArray();
        }

        private string GetServiceName(Type serviceType)
        {
            return serviceType.Name.Replace("DataService", string.Empty);
        }
    }
}