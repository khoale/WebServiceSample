namespace WebAPIOData
{
    using System;
    using System.Linq;

    using SimpleInjector;

    using WebAPIOData.Initializations;
    using WebAPIOData.Registrations;

    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var container = new Container();

            var registrationTypes = typeof(WebAPIRegistration).Assembly.GetTypes()
                    .Where(x => x.GetInterface(typeof(IRegistration).FullName) != null);
            foreach (var type in registrationTypes)
            {
                var registration = (IRegistration)Activator.CreateInstance(type);
                registration.Register(container);
            }

            container.GetInstance<AsyncStarter>().Start();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }
    }
}