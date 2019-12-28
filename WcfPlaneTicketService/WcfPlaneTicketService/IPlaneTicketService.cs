using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;

namespace WcfPlaneTicketService
{
    [ServiceContract]
    public interface IPlaneTicketService
    { 
        [OperationContract]  
        [WebInvoke(
            Method = "GET",
            UriTemplate = "/flights/{userId}/{tokenValue}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]       
        Task<List<Route>> getFullUserFlightsInfo(string userId, string tokenValue);

        [OperationContract]
        [WebInvoke(
            Method = "GET",
            UriTemplate = "/flights/{tokenValue}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        Task<List<Route>> getFlightsInfoAsync(string tokenValue);

        [OperationContract]  
        [WebInvoke(Method = "POST", UriTemplate = "/addFlight/{userId}/{tokenValue}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        Task<Route> addFlightAsync(string userId, Route route, string tokenValue);

        [OperationContract] 
        [WebInvoke(Method = "POST", UriTemplate = "/updateFlight/{userId}/{tokenValue}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        Task<Route> updateFlightAsync(string userId,  Route route, string tokenValue);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/deleteFlight/{userId}/{routeId}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        Task<int> deleteFlightAsync(string userId, string routeId, string tokenValue);
    }

    [DataContract]
    public class Token
    {
        [DataMember]
        public string token { get; set; }
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
}
