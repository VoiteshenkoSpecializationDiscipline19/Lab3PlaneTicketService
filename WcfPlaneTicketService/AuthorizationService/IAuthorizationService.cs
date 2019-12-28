using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;

namespace AuthorizationService
{
    [ServiceContract]
    public interface IAuthorizationService
    {
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            UriTemplate = "/{userId}/{tokenValue}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        Task<User> getUserAsync(string userId, string tokenValue); // userId = Email
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
