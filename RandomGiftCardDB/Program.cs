using System;

using MySql.Data;
using MySql.Data.MySqlClient;


namespace RandomGiftCardDB
{
    class Program
    {
        static void Main(string[] args)
        {
            string myDb = "server=localhost;userid=application;password=Database1234;database=rgcg";

            MySqlConnection myConn = new MySqlConnection(myDb);
            try
            {
                myConn.Open();

                string sql = "SELECt * FROM name";

                MySqlCommand cmd = new MySqlCommand(sql, myConn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine("{0} \n", rdr.GetString(0));
                }

                myConn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            myConn.Close();
        }
    }
}
