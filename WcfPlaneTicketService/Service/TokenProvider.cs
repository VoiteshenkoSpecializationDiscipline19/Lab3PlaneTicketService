using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace WcfPlaneTicketService.Service
{
    public class TokenProvider : ITokenProvider
    {
        private readonly string paymentServiceUri = ConfigurationManager.AppSettings["paymentUri"];
        private HttpClient client = new HttpClient();

        public async Task<string> GetToken(string forMethod)
        {
            string response = await client.GetStringAsync(new Uri(paymentServiceUri + forMethod));
            return JsonConvert.DeserializeObject<string>(response);
        }
    }
}