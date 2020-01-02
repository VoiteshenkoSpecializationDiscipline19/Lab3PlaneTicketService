using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WcfPlaneTicketService.Service
{
    public class DatabaseProvider : IDatabaseProvider
    {
        public void Modify(string sql)
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.AppSettings["dbConnectionString"]);
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception) { }
            conn.Close();
        }

        public Route SelectRoute(string where)
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.AppSettings["dbConnectionString"]);
            Route rt = new Route();
            string sql = "SELECT * FROM Route " + where + ";";

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                rdr.Read();
                rt.routeId = rdr[0].ToString();
                rt.routeFrom = rdr[1].ToString();
                rt.routeWhere = rdr[2].ToString();
                rt.routeDate = rdr[3].ToString();
                rt.routeTime = rdr[4].ToString();
                rt.routePrice = rdr[5].ToString();
                rdr.Close();
            }
            catch (Exception ex) { }
            conn.Close();

            return rt;
        }

        public List<Route> SelectAllRoutes()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.AppSettings["dbConnectionString"]);
            List<Route> resRoutes = new List<Route>();
            string sql = "SELECT * FROM Route;";

            try
            {
                conn.Open();
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
            catch (Exception ex) { }
            conn.Close();

            return resRoutes;
        }

        public List<Route> SelectRoutesByUser(string userId)
        {
            List<Route> resRoutes = new List<Route>();
            List<string> ids = new List<string>();
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.AppSettings["dbConnectionString"]);

            try
            {
                conn.Open();
                string sql = "SELECT userFlightRouteId FROM UserFlight WHERE userFlightUserId='" + userId + "';";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ids.Add(rdr[0].ToString());
                }
            }
            catch (Exception ex) { }
            conn.Close();

            foreach (string id in ids)
            {
                Route route = SelectRoute("WHERE routeId='" + id + "'");
                resRoutes.Add(route);
            }

            return resRoutes;
        }
    }
}