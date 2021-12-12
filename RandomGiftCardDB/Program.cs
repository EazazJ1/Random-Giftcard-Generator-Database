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
                names = myFunctions.getNames(myConn);
                
                giftCards = myFunctions.getCardTypes(myConn);

                //for (int i = 0; i < 10; i++)
                //{
                //    Console.WriteLine("{0}", giftCards[i]);
                //}


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            myConn.Close();

            string[] randomCards = new string[60];

            randomCards = myFunctions.makeRandom(names, giftCards);

            for (int i = 0; i < 65; i++)
            {
                Console.WriteLine("{0}", randomCards[i]);
            }

            //var Rand = new Random();
        }
    }
}
