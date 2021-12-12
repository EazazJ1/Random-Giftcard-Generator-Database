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
            string[] randomCards = new string[60];

            MySqlConnection myConn = new MySqlConnection(myDb);
            try
            {                
                names = myFunctions.getNames(myConn);
                
                giftCards = myFunctions.getCardTypes(myConn);               

                randomCards = myFunctions.makeRandom(names, giftCards);

                //for (int i = 0; i < 10; i++)
                //{
                //    Console.WriteLine("{0}", giftCards[i]);
                //}

                //string create = "CREATE TABLE RANDOM (Name varchar(25), Type varchar(25), Amount int);";
                //MySqlCommand cmd = new MySqlCommand(create, myConn);
                //myConn.Open();

                //cmd.ExecuteNonQuery();
                //myConn.Close();

                //myFunctions.createRandomTable(myConn);

                //myFunctions.dropRandomTable(myConn);

                //myFunctions.AddToRandomTable(myConn, randomCards);

                //myFunctions.getTotalAmount(myConn);

                int average = myFunctions.getAverageAmount(myConn);
                Console.WriteLine("{0}", average);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            myConn.Close();

            

            //for (int i = 0; i < 60; i++)
            //{
            //    Console.WriteLine("{0}", randomCards[i]);
            //}

        }
    }
}
