using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace WcfPlaneTicketService
{
    public class PlaneTicketService : IPlaneTicketService
    {
        private const string connStr = "server=remotemysql.com;Port=3306;Database=onWm52J7I5;Uid=onWm52J7I5;Pwd=TWgylKHgPf";
        private MySqlConnection conn = new MySqlConnection(connStr);

        public User getUser(string userId, string tokenValue)
        {
            User user = new User();

            string methodName = "getUser";
            string tokVal = getToken(methodName);

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
                catch (Exception ex)
                { }

                conn.Close();
            }
            

            return user;
        }

        public List<Route> getFullUserFlightsInfo(string userId, string tokenValue)
        {
            List<Route> resRoutes = new List<Route>();

            string methodName = "getFullUserFlightsInfo";
            string tokVal = getToken(methodName);

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
                    catch (Exception ex)
                    { }

                    conn.Close();
                }
            return resRoutes;
        }

        public List<Route> getFlightsInfo(string tokenValue)
        {
            List<Route> resRoutes = new List<Route>();

            string methodName = "getFlightsInfo";
            string tokVal = getToken(methodName);

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
                catch (Exception ex)
                { }

                conn.Close();
            }
            return resRoutes;
        }

        public Route addFlight(string userId, Route route, string tokenValue)
        {

            string methodName = "addFlight";
            string tokVal = getToken(methodName);

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
                    catch (Exception ex)
                    { }

                    conn.Close();
                }

            return route;
        }

        public Route updateFlight(string userId, Route route, string tokenValue)
        {

            string methodName = "updateFlight";
            string tokVal = getToken(methodName);

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

                        sql = "UPDATE UserFlight SET userFlightRouteId='" +
                            routeId + "' WHERE userFlightUserId='" + userId + "';";

                        cmd = new MySqlCommand(sql, conn);
                        cmd.ExecuteNonQuery();

                        route.routeId = routeId;
                        route.routeTime = routeTime;
                        route.routePrice = routePrice;

                    }
                    catch (Exception ex)
                    { }


                    conn.Close();
                }

            return route;
        }

        public int deleteFlight(string userId, string routeId, string tokenValue)
        {
            int successCode = -1;
            //string tokVal = tokens["deleteFlight"].tokenValue;
            //string tokVal = "token1234";

            string methodName = "deleteFlight";
            string tokVal = getToken(methodName);

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
                catch (Exception ex)
                { }

                conn.Close();
                successCode = 0;
            }

            return successCode;
        }

        public void setToken(string method, Token token)
        {
            try
            {
                conn.Open();
                string sql = "UPDATE Token SET tokenValue='" +
                            token.tokenValue + "' WHERE methodName='" + method + "';";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                sql = "UPDATE Token SET dateFrom='" +
                            token.date_from + "' WHERE methodName='" + method + "';";
                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                sql = "UPDATE Token SET dateTo='" +
                            token.date_to + "' WHERE methodName='" + method + "';";
                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { }

            conn.Close();
        }

        private string getToken(string methodName)
        {
            string token = "", dateFrom = "", dateTo = "";
            try
            {
                conn.Open();
                string sql_tok = "";
                MySqlCommand cmd_tok;
                MySqlDataReader rdr_tok;
             
                sql_tok = "SELECT tokenValue, dateFrom, dateTo FROM Token WHERE methodName='" + methodName + "';";
                cmd_tok = new MySqlCommand(sql_tok, conn);
                rdr_tok = cmd_tok.ExecuteReader();
                rdr_tok.Read();
                
                dateFrom = rdr_tok[1].ToString();
                dateTo = rdr_tok[2].ToString();
                
                DateTime currentDate = DateTime.Now;
                string dateString = currentDate.ToString("d");
                currentDate = Convert.ToDateTime(dateString);
                DateTime dateFromDate = Convert.ToDateTime(dateFrom);
                DateTime dateToDate = Convert.ToDateTime(dateTo);

                if (currentDate >= dateFromDate && currentDate <= dateToDate)
                    token = rdr_tok[0].ToString();

                rdr_tok.Close();
            }
            catch (Exception ex)
            { }

            conn.Close();
            return token;
        }
    }
}
