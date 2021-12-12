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

            string[] names = new string[10];
            string[] giftCards = new string[10];

            MySqlConnection myConn = new MySqlConnection(myDb);
            try
            {
                //Connecting to db and getting the names table and storing in a string array called names
                myConn.Open();
                string getNames = "SELECT * FROM name"; 
                MySqlCommand getNamesCmd = new MySqlCommand(getNames, myConn);

                MySqlDataReader rdr = getNamesCmd.ExecuteReader();
                int count = 0;

                Console.WriteLine("Names:");

                while (rdr.Read())
                {
                    names[count] = rdr.GetString(0);
                    Console.WriteLine("{0}", names[count]);
                    count++;                   
                }
                myConn.Close();


                //Connecting to db and getting the gift card types and storing in a string array called giftcards
                myConn.Open();
                string getCardTypes = "SELECT * FROM giftcard";

                MySqlCommand getCardsCmd = new MySqlCommand(getCardTypes, myConn);

                MySqlDataReader rdrTwo = getCardsCmd.ExecuteReader();

                count = 0;
                Console.WriteLine("\n\nGiftCard Types:");

                while (rdrTwo.Read())
                {
                    giftCards[count] = rdrTwo.GetString(0);
                    Console.WriteLine("{0}", giftCards[count]);
                    count++;
                }
                myConn.Close();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            myConn.Close();

            //var Rand = new Random();
        }
    }
}
