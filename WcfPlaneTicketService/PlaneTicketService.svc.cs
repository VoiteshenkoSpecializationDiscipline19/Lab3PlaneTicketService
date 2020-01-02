using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.ServiceModel.Activation;
using System.Threading.Tasks;
using WcfPlaneTicketService.Service;

namespace WcfPlaneTicketService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class PlaneTicketService : IPlaneTicketService
    {

        private readonly ITokenProvider _tokenProvider;
        private readonly IDatabaseProvider _databaseProvider;

        public PlaneTicketService(ITokenProvider tokenProvider, IDatabaseProvider databaseProvider)
        {
            _tokenProvider = tokenProvider;
            _databaseProvider = databaseProvider;
        }

        public async Task<List<Route>> getFullUserFlightsInfo(string userId, string tokenValue)
        {
            List<Route> resRoutes = new List<Route>();
            string tokVal = await _tokenProvider.GetToken("getFullUserFlightsInfo");

            if (tokVal.Equals(tokenValue))
            {
                resRoutes = _databaseProvider.SelectRoutesByUser(userId);
            }
            return resRoutes;
        }

        public async Task<List<Route>> getFlightsInfoAsync(string tokenValue)
        {
            List<Route> resRoutes = new List<Route>();
            string tokVal = await _tokenProvider.GetToken("getFlightsInfo");

            if (tokVal.Equals(tokenValue))
            {
                resRoutes = _databaseProvider.SelectAllRoutes();
            }
            return resRoutes;
        }

        public async Task<Route> addFlightAsync(string userId, Route route, string tokenValue)
        {
            string tokVal = await _tokenProvider.GetToken("addFlight");

            if (tokVal.Equals(tokenValue))
            {
                Route rt = _databaseProvider.SelectRoute("WHERE routeFrom='" + route.routeFrom +
                        "' AND routeWhere='" + route.routeWhere +
                        "' AND routeDate='" + route.routeDate + "'");
                route = rt;

                _databaseProvider.Modify("INSERT INTO UserFlight(userFlightRouteId, userFlightUserId) VALUES('" +
                            rt.routeId + "','" + userId + "');");
            }

            return route;
        }

        public async Task<Route> updateFlightAsync(string userId, Route route, string tokenValue)
        {
            string tokVal = await _tokenProvider.GetToken("updateFlight");

            if (tokVal.Equals(tokenValue))
            {
                string oldRouteId = route.routeId;

                Route rt = _databaseProvider.SelectRoute("WHERE routeFrom='" + route.routeFrom +
                        "' AND routeWhere='" + route.routeWhere +
                        "' AND routeDate='" + route.routeDate + "'");
                route = rt;

                _databaseProvider.Modify("UPDATE UserFlight SET userFlightRouteId='" +
                            route.routeId + "' WHERE userFlightUserId='" + userId
                            + "' AND userFlightRouteId='" + oldRouteId + "';");
            }

            return route;
        }

        public async Task<int> deleteFlightAsync(string userId, string routeId, string tokenValue)
        {
            int successCode = -1;

            string tokVal = await _tokenProvider.GetToken("deleteFlight");

            if (tokVal.Equals(tokenValue))
            {
                _databaseProvider.Modify("DELETE FROM UserFlight WHERE userFlightRouteId='" + routeId
                            + "' AND userFlightUserId='" + userId + "';");
                successCode = 0;
            }

            return successCode;
        }
    }
}
