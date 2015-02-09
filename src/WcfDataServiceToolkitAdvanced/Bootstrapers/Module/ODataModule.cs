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

    using WcfDataServiceToolkitAdvanced.Extensions;
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
        private readonly ILifetimeScope rootScope;

        private readonly ODataServiceHostFactory serviceHostFactory;

        private readonly RepositoryFactory repositoryFactory;

        public ODataStartable(ILifetimeScope rootScope, ODataServiceHostFactory serviceHostFactory, RepositoryFactory repositoryFactory)
        {
            this.rootScope = rootScope;
            this.serviceHostFactory = serviceHostFactory;
            this.repositoryFactory = repositoryFactory;
        }

        protected override void OnStart()
        {
            AutofacHostFactory.Container = this.rootScope;
            this.RegisterRouteTable();
            this.RegisterRepositories();
        }

        private void RegisterRepositories()
        {
            //// Register resource manualy
            // this.repositoryFactory
            //     .RegisterResource<PersonDto, PeopleRepository>();

            // Register using reflection
            var repositoryTypes = this.GetAllRepositoryTypes();
            Debug.Assert(repositoryTypes.Length > 0, "repositoryTypes.Length > 0");
            var registerSourceMethod = typeof(RepositoryFactory).GetMethod("RegisterResource");

            foreach (var repositoryType in repositoryTypes)
            {
                Debug.Assert(repositoryType.BaseType != null, "repositoryType.BaseType != null");
                var resourceType = repositoryType.BaseType.GetGenericArguments()[0];
                var genericRegisterSourceMethod =
                    registerSourceMethod.MakeGenericMethod(resourceType, repositoryType);

                genericRegisterSourceMethod.Invoke(this.repositoryFactory, null);
            }
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

        private Type[] GetAllRepositoryTypes()
        {
            return typeof(RepositoryBase<,>).Assembly.GetTypes()
                .Where(x => x.IsConcreteType())
                .Where(x => x.IsImplementGenericType(typeof(RepositoryBase<,>)))
                .ToArray();
        }

        private Type[] GetAllServiceTypes()
        {
            return typeof(PersonDataService).Assembly.GetTypes()
                .Where(x => x.IsConcreteType())
                .Where(x => x.IsImplementGenericType(typeof(ODataService<>)))
                .ToArray();
        }

        private string GetServiceName(Type serviceType)
        {
            return serviceType.Name.Replace("DataService", string.Empty);
        }
    }
}