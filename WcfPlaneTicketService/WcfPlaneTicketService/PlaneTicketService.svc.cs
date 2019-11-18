using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
//using MySql.Data.MySqlClient;

namespace WcfPlaneTicketService
{
    public class PlaneTicketService : IPlaneTicketService
    {
        private const string connStr = "server=remotemysql.com;Port=3306;Database=onWm52J7I5;Uid=onWm52J7I5;Pwd=TWgylKHgPf";
        private MySqlConnection conn = new MySqlConnection(connStr);

        public User getUser(string userId, string tokenValue)
        {
            // check token 

            User user = new User();

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
            catch (Exception ex)
            {}

            conn.Close();
            return user;
        }

        public List<Route> getFullUserFlightsInfo(string userId, string tokenValue)
        {
            // check token 

            List<Route> resRoutes = new List<Route>();
            // by userId from UserFlight get routeId, for each routeId from Route get info(without date and price)

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
            catch (Exception ex)
            { }
           
            conn.Close();

            return resRoutes;
        }

        public Route addFlight(string userId, Route route, string tokenValue)
        {
            // check token 

            try
            {
                conn.Open();

                string sql = "SELECT routeId FROM Route WHERE routeFrom='" + route.routeFrom +
                    "' AND routeWhere='" + route.routeWhere +
                    "' AND routeDate='" + route.routeDate +
                    "' AND routeTime='" + route.routeTime +
                    "' AND routePrice='" + route.routePrice;
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                string routeId = rdr[0].ToString();
                rdr.Close();

                sql = "INSERT INTO UserFlight(userFlightRouteId, userFlightUserId) VALUES('" +
                    routeId + "','" + userId + "');";
                cmd = new MySqlCommand(sql, conn);
                rdr = cmd.ExecuteReader();
                rdr.Read();

                route.routeId = routeId;

                rdr.Close();
            }
            catch (Exception ex)
            { }

            conn.Close();
            return route;
        }

        public Route updateFlight(string userId, Route route, string tokenValue)
        {
            // check token 

            try
            {
                conn.Open();

                string sql = "SELECT routeId FROM Route WHERE routeFrom='" + route.routeFrom +
                    "' AND routeWhere='" + route.routeWhere +
                    "' AND routeDate='" + route.routeDate +
                    "' AND routeTime='" + route.routeTime +
                    "' AND routePrice='" + route.routePrice;
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                string routeId = rdr[0].ToString();
                rdr.Close();

                sql = "UPDATE UserFlight SET userFlightRouteId='" +
                    routeId + "' WHERE userFlightUserId='" + userId + "');";

                cmd = new MySqlCommand(sql, conn);
                rdr = cmd.ExecuteReader();
                rdr.Read();

                route.routeId = routeId;

                rdr.Close();
            }
            catch (Exception ex)
            { }

            conn.Close();
            return route;
        }

        public void deleteFlight(string userId, string routeId, string tokenValue)
        {
            // check token 

            try
            {
                conn.Open();

                string sql = "DELETE FROM UserFlight WHERE userFlightRouteId='" + routeId 
                    + "' AND userFlightUserId='" + userId + "');";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                rdr.Read();

                rdr.Close();
            }
            catch (Exception ex)
            { }

            conn.Close();
        }

        public List<Token> tokens = new List<Token>();

        public void setToken(string _methodName, string _tokenValue)
        {
            Token tempToken = new Token();
            tempToken.methodName = _methodName;
            tempToken.tokenValue = _tokenValue;
            tokens.Add(tempToken);
        }
    }
}
