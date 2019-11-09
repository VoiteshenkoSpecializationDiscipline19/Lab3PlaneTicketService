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
        public User getUser(string userId)
        {
            User user = new User();
            // get user from db by userId
            user.userId = "user1@gmail.com";
            user.userFirstName = "user1";
            user.userSecondName = "user1";
            return user;
        }

        public List<Route> getFullUserFlightsInfo(string userId)
        {
            List<Route> resRoutes = new List<Route>();
            // by userId from UserFlight get routeId, for each routeId from Route get info(without date and price)

            // test
            Route route = new Route();
            route.routeId = 1.ToString();
            route.routeFrom = "Minsk";
            route.routeWhere = "London";
            route.routeDate = "01.01.2020";
            route.routeTime = "00:00";
            route.routePrice = "30";
            resRoutes.Add(route);
            return resRoutes;
        }

        public void addFlight(string userId, string routeId, Route route)
        {

        }

        public Route updateFlight(string userId, string routeId, Route route)
        {
            Route updatedRoute = new Route();
            //
            return updatedRoute;
        }

        public void deleteFlight(string userId, string routeId)
        {

        }

        public List<Token> tokens = new List<Token>();

        public void setToken(string _methodName, string _tokenValue)
        {
            Token tempToken = new Token();
            tempToken.methodName = _methodName;
            tempToken.tokenValue = _tokenValue;
            tokens.Add(tempToken);
        }
    }
}
