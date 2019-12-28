using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationService
{
    public class AuthorizationService : IAuthorizationService
    {
        private MySqlConnection conn = new MySqlConnection(ConfigurationManager.AppSettings["dbConnectionString"]);
        private readonly string paymentServiceUri = ConfigurationManager.AppSettings["paymentUri"];
        private HttpClient client = new HttpClient();
        public async Task<User> getUserAsync(string userId, string tokenValue)
        {         
            User user = new User();

            string methodName = "getUser";
            string response = await client.GetStringAsync(new Uri(paymentServiceUri + methodName));
            string tokVal = JsonConvert.DeserializeObject<string>(response);

            if (tokVal.Equals(tokenValue))
            {
                try
                {
                    conn.Open();

                    string sql = "SELECT * FROM User WHERE userId='" + userId + "';";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    rdr.Read();

                    user.userId = rdr[0].ToString();
                    user.userFirstName = rdr[1].ToString();
                    user.userSecondName = rdr[2].ToString();

                    rdr.Close();
                }
                catch (Exception) { }

                conn.Close();
            }


            return user;
        }
    }
}
