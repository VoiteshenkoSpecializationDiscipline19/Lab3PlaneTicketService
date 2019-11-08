using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfPlaneTicketService
{
    public class PlaneTicketService : IPlaneTicketService
    {
        public List<Route> getUserFlightsInfo(string userId)
        {
            List<Route> resRoutes = new List<Route>();
            // by userId from UserFlight get routeId, for each routeId from Route get info(without date and price)

            // test
            Route route = new Route();
            route.routeId = 1.ToString();
            route.routeFrom = "Minsk";
            route.routeWhere = "London";
            route.routeDate = "01.01.2020";
            //route.routeTime = "00:00";
            //route.routePrice = "30";
            resRoutes.Add(route);
            return resRoutes;
        }

        public List<Route> getFullUserFlightsInfo(string userId)
        {
            List<Route> resRoutes = new List<Route>();
            // by userId from UserFlight get routeId, for each routeId from Route get info(with date and price)
            return resRoutes;
        }

        public void addFlight(string userId, string parameters)
        {

        }

        public void updateFlight(string userId, string userFlightId, string parameters)
        {

        }

        public void deleteFlight(string userId, string userFlightId)
        {

        }
    }
}
