using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace WcfPlaneTicketService
{
    public class PlaneTicketService : IPlaneTicketService
    {
        private MySqlConnection conn = new MySqlConnection(ConfigurationManager.AppSettings["dbConnectionString"]);
        private readonly string paymentServiceUri = ConfigurationManager.AppSettings["paymentUri"];
        private HttpClient client = new HttpClient();

        public async Task<List<Route>> getFullUserFlightsInfo(string userId, string tokenValue)
        {
            List<Route> resRoutes = new List<Route>();

            string methodName = "getFullUserFlightsInfo";
            string response = await client.GetStringAsync(new Uri(paymentServiceUri + methodName));
            string tokVal = JsonConvert.DeserializeObject<string>(response);

            if (tokVal.Equals(tokenValue))
            {
                try
                    {
                        conn.Open();

                        string sql = "SELECT userFlightRouteId FROM UserFlight WHERE userFlightUserId='" + userId + "';";
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        MySqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            Route route = new Route();
                            route.routeId = rdr[0].ToString();
                            resRoutes.Add(route);
                        }
                        rdr.Close();

                        foreach (Route rt in resRoutes)
                        {
                            sql = "SELECT * FROM Route WHERE routeId='" + rt.routeId + "';";
                            cmd = new MySqlCommand(sql, conn);
                            rdr = cmd.ExecuteReader();

                            while (rdr.Read())
                            {
                                rt.routeFrom = rdr[1].ToString();
                                rt.routeWhere = rdr[2].ToString();
                                rt.routeDate = rdr[3].ToString();
                                rt.routeTime = rdr[4].ToString();
                                rt.routePrice = rdr[5].ToString();

                            }
                            rdr.Close();
                        }


                    }
                    catch (Exception) { }

                    conn.Close();
                }
            return resRoutes;
        }

        public async Task<List<Route>> getFlightsInfoAsync(string tokenValue)
        {
            List<Route> resRoutes = new List<Route>();

            string methodName = "getFlightsInfo";
            string response = await client.GetStringAsync(new Uri(paymentServiceUri + methodName));
            string tokVal = JsonConvert.DeserializeObject<string>(response);

            if (tokVal.Equals(tokenValue))
            {
                try
                {
                    conn.Open();

                    
                        string sql = "SELECT * FROM Route;";
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        MySqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            Route rt = new Route();
                            rt.routeId = rdr[0].ToString();
                            rt.routeFrom = rdr[1].ToString();
                            rt.routeWhere = rdr[2].ToString();
                            rt.routeDate = rdr[3].ToString();
                            rt.routeTime = rdr[4].ToString();
                            rt.routePrice = rdr[5].ToString();
                            resRoutes.Add(rt);
                    }
                    rdr.Close();

                }
                catch (Exception){ }

                conn.Close();
            }
            return resRoutes;
        }

        public async Task<Route> addFlightAsync(string userId, Route route, string tokenValue)
        {

            string methodName = "addFlight";
            string response = await client.GetStringAsync(new Uri(paymentServiceUri + methodName));
            string tokVal = JsonConvert.DeserializeObject<string>(response);

            if (tokVal.Equals(tokenValue))
            {

                try
                    {
                        conn.Open();
                        string sql = "SELECT routeId, routeTime, routePrice FROM Route WHERE routeFrom='" + route.routeFrom +
                            "' AND routeWhere='" + route.routeWhere +
                            "' AND routeDate='" + route.routeDate + "';";
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        MySqlDataReader rdr = cmd.ExecuteReader();


                        string routeId = "", routeTime = "", routePrice = "";

                        rdr.Read();

                        routeId = rdr[0].ToString();
                        routeTime = rdr[1].ToString();
                        routePrice = rdr[2].ToString();
                        rdr.Close();

                        sql = "INSERT INTO UserFlight(userFlightRouteId, userFlightUserId) VALUES('" +
                            routeId + "','" + userId + "');";
                        cmd = new MySqlCommand(sql, conn);
                        cmd.ExecuteNonQuery();

                        route.routeId = routeId;
                        route.routeTime = routeTime;
                        route.routePrice = routePrice;

                    }
                    catch (Exception){ }

                    conn.Close();
                }

            return route;
        }

        public async Task<Route> updateFlightAsync(string userId, Route route, string tokenValue)
        {

            string methodName = "updateFlight";
            string response = await client.GetStringAsync(new Uri(paymentServiceUri + methodName));
            string tokVal = JsonConvert.DeserializeObject<string>(response);

            if (tokVal.Equals(tokenValue))
            {
                try
                    {
                        conn.Open();

                        string sql = "SELECT routeId, routeTime, routePrice FROM Route WHERE routeFrom='" + route.routeFrom +
                        "' AND routeWhere='" + route.routeWhere +
                        "' AND routeDate='" + route.routeDate + "';";
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        MySqlDataReader rdr = cmd.ExecuteReader();
                        string routeId = "", routeTime = "", routePrice = "";

                        rdr.Read();
                        string oldRouteId = route.routeId;    

                        routeId = rdr[0].ToString();
                        routeTime = rdr[1].ToString();
                        routePrice = rdr[2].ToString();
                        rdr.Close();

                        sql = "UPDATE UserFlight SET userFlightRouteId='" +
                            routeId + "' WHERE userFlightUserId='" + userId
                            + "' AND userFlightRouteId='" + oldRouteId + "';";

                        cmd = new MySqlCommand(sql, conn);
                        int c = cmd.ExecuteNonQuery();

                        route.routeId = routeId;
                        route.routeTime = routeTime;
                        route.routePrice = routePrice;

                    }
                    catch (Exception){ }


                    conn.Close();
                }

            return route;
        }

        public async Task<int> deleteFlightAsync(string userId, string routeId, string tokenValue)
        {
            int successCode = -1;

            string methodName = "deleteFlight";
            string response = await client.GetStringAsync(new Uri(paymentServiceUri + methodName));
            string tokVal = JsonConvert.DeserializeObject<string>(response);

            if (tokVal.Equals(tokenValue))
            {

                try
                    {
                        conn.Open();
                        string sql = "DELETE FROM UserFlight WHERE userFlightRouteId='" + routeId
                            + "' AND userFlightUserId='" + userId + "';";
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        cmd.ExecuteNonQuery();
                }
                catch (Exception){ }

                conn.Close();
                successCode = 0;
            }

            return successCode;
        }
    }
}
