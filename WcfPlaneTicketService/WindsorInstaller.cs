using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using WcfPlaneTicketService.Service;

namespace WcfPlaneTicketService
{
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IPlaneTicketService, PlaneTicketService>(),
                Component.For<ITokenProvider, TokenProvider>(),
                Component.For<IDatabaseProvider, DatabaseProvider>());
        }
    }
}