namespace WebAPIOData.Registrations
{
    using System.Web.Http;

    using SimpleInjector;

    public class WebAPIRegistration : IRegistration
    {
        public void Register(Container container)
        {
            container.Register(() => GlobalConfiguration.Configuration);

            ODataRegistration.Register(container);
        }

        private static class ODataRegistration
        {
            public static void Register(Container container)
            {
            }
        }
    }
}