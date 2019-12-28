using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace PaymentService
{
    public class PaymentService : IPaymentService
    {
        private MySqlConnection conn = new MySqlConnection(ConfigurationManager.AppSettings["dbConnectionString"]);
        public string getToken(string methodName)
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
            catch (Exception) { }

            conn.Close();
            return token;
        }
        

        public void setToken(string methodName, Token token)
        {
            try
            {
                conn.Open();
                string sql = "UPDATE Token SET tokenValue='" +
                            token.token + "' WHERE methodName='" + methodName + "';";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                sql = "UPDATE Token SET dateFrom='" +
                            token.date_from + "' WHERE methodName='" + methodName + "';";
                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                sql = "UPDATE Token SET dateTo='" +
                            token.date_to + "' WHERE methodName='" + methodName + "';";
                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception) { }

            conn.Close();
        }
    }
}
