using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfPlaneTicketService
{
    [ServiceContract]
    public interface IPlaneTicketService
    {
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            UriTemplate = "/flights/{userId}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]        
        List<Route> getUserFlightsInfo(string userId); // without date and price 

        [OperationContract]  
        [WebInvoke(
            Method = "GET",
            UriTemplate = "/flights/{userId}/details",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]       
        List<Route> getFullUserFlightsInfo(string userId); // with date and price      

        [OperationContract]  //?? что из клиента получаю? parameters - from + where + date + time + price ??
        [WebInvoke(Method = "POST", UriTemplate = "/addFlight/{userId}/{parameters}")]
        void addFlight(string userId, string parameters);

        [OperationContract]  //?? что из клиента получаю? parameters - from + where + date + time + price ??
        [WebInvoke(Method = "POST", UriTemplate = "/updateFlight/{userId}/{userFlightId}/{parameters}")]
        void updateFlight(string userId, string userFlightId, string parameters);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/deleteFlight/{userId}/{userFlightId}")]
        void deleteFlight(string userId, string userFlightId);
    }

    public class Route
    {
        public string routeId { get; set; }
        public string routeFrom { get; set; }
        public string routeWhere { get; set; }
        public string routeDate { get; set; }
        public string routeTime { get; set; }
        public string routePrice { get; set; }
    }

    public class UserFlight
    {
        public string userFlightRouteId { get; set; }
        public string userFlightUserId { get; set; }
    }

    public class User
    {
        public string userId { get; set; }
        public string userEmail { get; set; }
        public string userFirstName { get; set; }
        public string userSecondName { get; set; }
    }
}
