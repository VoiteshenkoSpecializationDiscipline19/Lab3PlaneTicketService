using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfPlaneTicketService.Service
{
    [ServiceContract]
    public interface ITokenProvider
    {
        [OperationContract]
        Task<string> GetToken(string forMethod);
    }
}
