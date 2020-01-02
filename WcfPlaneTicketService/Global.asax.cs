using Castle.Facilities.WcfIntegration;
using Castle.Windsor;
using System;
using WcfPlaneTicketService;

namespace WcfPlaneTicketService
{
    public partial class Global : System.Web.HttpApplication
    {
        static IWindsorContainer container;

        protected void Application_Start(Object sender, EventArgs e)
        {
            container = new WindsorContainer();
            container.AddFacility<WcfFacility>();
            container.Install(new WindsorInstaller()); //The class you just created.
        }
    }
}