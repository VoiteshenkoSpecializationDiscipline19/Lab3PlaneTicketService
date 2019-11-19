using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using MySql.Data.MySqlClient;

namespace WcfPlaneTicketService
{
    [ServiceContract]
    public interface IPlaneTicketService
    {
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            UriTemplate = "/{userId}/{tokenValue}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        User getUser(string userId, string tokenValue); // userId = Email

        [OperationContract]  
        [WebInvoke(
            Method = "GET",
            UriTemplate = "/flights/{userId}/{tokenValue}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]       
        List<Route> getFullUserFlightsInfo(string userId, string tokenValue);

        [OperationContract]
        [WebInvoke(
            Method = "GET",
            UriTemplate = "/flights/{tokenValue}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        List<Route> getFlightsInfo(string tokenValue);

        [OperationContract]  
        [WebInvoke(Method = "POST", UriTemplate = "/addFlight/{userId}/{tokenValue}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        Route addFlight(string userId, Route route, string tokenValue);

        [OperationContract] 
        [WebInvoke(Method = "POST", UriTemplate = "/updateFlight/{userId}/{tokenValue}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        Route updateFlight(string userId,  Route route, string tokenValue);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/deleteFlight/{userId}/{routeId}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        int deleteFlight(string userId, string routeId, string tokenValue);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/setToken/{method}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        void setToken(string method, Token token);
    }

    [DataContract]
    public class Token
    {
        [DataMember]
        public string tokenValue { get; set; }
        [DataMember]
        public string date_from { get; set; }
        [DataMember]
        public string date_to { get; set; }
    }

    [DataContract]
    public class Route
    {
        [DataMember]
        public string routeId { get; set; }
        [DataMember]
        public string routeFrom { get; set; }
        [DataMember]
        public string routeWhere { get; set; }
        [DataMember]
        public string routeDate { get; set; }
        [DataMember]
        public string routeTime { get; set; }
        [DataMember]
        public string routePrice { get; set; }
    }

    [DataContract]
    public class UserFlight
    {
        [DataMember]
        public string userFlightRouteId { get; set; }
        [DataMember]
        public string userFlightUserId { get; set; }
    }

    [DataContract]
    public class User
    {
        [DataMember]
        public string userId { get; set; }   // Email = Id
        [DataMember]
        public string userFirstName { get; set; }
        [DataMember]
        public string userSecondName { get; set; }
    }
}
