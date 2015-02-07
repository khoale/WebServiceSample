namespace WcfDataServiceToolkitAdvanced.Bootstrapers
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.ServiceModel.Activation;
    using System.Web.Routing;

    using Autofac;
    using Autofac.Integration.Wcf;

    using Microsoft.Data.Services.Toolkit;

    using WcfDataServiceToolkitAdvanced.Dto;
    using WcfDataServiceToolkitAdvanced.Repositories;
    using WcfDataServiceToolkitAdvanced.Services;

    public class ODataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ODataServiceHostFactory>().SingleInstance();
            builder.RegisterType<ODataStartable>().As<IAsyncStartable>();
        }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed. Suppression is OK here.")]
    public class ODataStartable : AsyncStartable
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

        protected override void OnStart()
        {
            AutofacHostFactory.Container = this.componentContext.Resolve<ILifetimeScope>();
            this.RegisterRouteTable();
            this.RegisterRepositories();
        }

        private void RegisterRepositories()
        {
            this.repositoryFactory
                .RegisterResource<PersonDto>().As<PeopleRepository>();
        }

        private void RegisterRouteTable()
        {
            var serviceTypes = this.GetAllServiceTypes();
            Debug.Assert(serviceTypes.Length > 0, "serviceTypes.Length > 0");

            foreach (var serviceType in serviceTypes)
            {
                var serviceName = this.GetServiceName(serviceType);
                var prefix = string.Format("api/{0}", serviceName).ToLower();
                RouteTable.Routes.Add(new ServiceRoute(prefix, this.serviceHostFactory, serviceType));
            }
        }

        private Type[] GetAllServiceTypes()
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