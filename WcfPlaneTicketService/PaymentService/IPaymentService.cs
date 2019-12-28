using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace PaymentService
{
    [ServiceContract]
    public interface IPaymentService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/setToken/{methodName}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        void setToken(string methodName, Token token);

        [OperationContract]
        [WebInvoke(
           Method = "GET",
           UriTemplate = "/getToken/{methodName}",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        string getToken(string methodName);
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
}