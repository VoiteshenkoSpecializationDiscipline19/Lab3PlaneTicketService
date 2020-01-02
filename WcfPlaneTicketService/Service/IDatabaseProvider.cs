using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfPlaneTicketService.Service
{
    public interface IDatabaseProvider
    {
        void Modify(string sql);
        Route SelectRoute(string where);
        List<Route> SelectAllRoutes();
        List<Route> SelectRoutesByUser(string userId);
    }
}
