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

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            myConn.Close();
        }
    }
}
