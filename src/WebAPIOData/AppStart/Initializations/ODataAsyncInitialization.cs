namespace WebAPIOData.Initializations
{
    using System.Web.Http;
    using System.Web.OData.Builder;
    using System.Web.OData.Extensions;
    using System.Web.OData.Routing;
    using System.Web.OData.Routing.Conventions;

    using Microsoft.OData.Edm;

    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;

    using WebAPIOData.Dtos;

    public class ODataAsyncInitialization : AsyncInitialization
    {
        private readonly Container container;

        private readonly HttpConfiguration httpConfiguration;

        private readonly ODataConventionModelBuilder builder;

        public ODataAsyncInitialization(Container container, HttpConfiguration httpConfiguration)
        {
            this.container = container;
            this.httpConfiguration = httpConfiguration;
            this.builder = new ODataConventionModelBuilder(this.httpConfiguration);
        }

        protected override void OnStart()
        {
            // this.conventionModelBuilder.EntitySet<PersonDto>("People");
            var edmModel = ODataModelBuilderHelper.GetEdmModel(this.builder);

            // Create the default collection of built-in conventions.
            // var conventions = ODataRoutingConventions.CreateDefault();

            // Insert the custom convention at the start of the collection.
            // conventions.Insert(0, new NavigationIndexRoutingConvention());
            // var pathHandler = new DefaultODataPathHandler();

            this.httpConfiguration.EnableCaseInsensitive(true);
            this.httpConfiguration.EnableUnqualifiedNameCall(true);
            this.httpConfiguration.EnableEnumPrefixFree(true);
            this.httpConfiguration.MapODataServiceRoute("ODataPersonRoute", "api/person", edmModel);

            this.httpConfiguration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(this.container);
            this.httpConfiguration.EnsureInitialized();
        }

        private static class ODataModelBuilderHelper
        {
            public static IEdmModel GetEdmModel(ODataConventionModelBuilder builder)
            {
                ////builder.Namespace = "WebAPISample";
                ////builder.ContainerName = "DefaultContainer";

                BuildEntitySets(builder);
                BuildFunctions(builder);

                return builder.GetEdmModel();
            }

            private static void BuildEntitySets(ODataConventionModelBuilder builder)
            {
                builder.EntitySet<PersonDto>("People");
                builder.EntitySet<CountryRegionDto>("CountryRegions");
            }

            private static void BuildFunctions(ODataConventionModelBuilder builder)
            {
                var countryRegionEntityType = builder.EntityType<CountryRegionDto>();

                countryRegionEntityType.Collection
                    .Function("TotalState")
                    .Returns<int>()
                    .Parameter<string>("CountryRegionCode");

                countryRegionEntityType
                    .Collection
                    .Function("Total")
                    .Returns<int>();
            }
        }
    }
}