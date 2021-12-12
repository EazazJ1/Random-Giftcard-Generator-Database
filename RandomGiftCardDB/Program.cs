using System;

using MySql.Data;
using MySql.Data.MySqlClient;



namespace RandomGiftCardDB
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] names = new string[10];
            string[] giftCards = new string[10];
            string[] randomCards = new string[60];

            string myDb = "server=localhost;userid=application;password=Database1234;database=rgcg";
            MySqlConnection myConn = new MySqlConnection(myDb);

            try
            {
                names = myFunctions.getNames(myConn);

                giftCards = myFunctions.getCardTypes(myConn);

                randomCards = myFunctions.makeRandom(names, giftCards);

                int killswitch = 0;

                while(killswitch == 0)
                {
                    myFunctions.menu();

                    string input = Console.ReadLine();
                    int choice = Convert.ToInt32(input);

                    switch (choice)
                    {
                        case 1:
                            Console.Clear();
                            myFunctions.AddToRandomTable(myConn, randomCards);
                            break;
                        case 2:
                            Console.Clear();
                            myFunctions.getRandomListFromDb(myConn);
                            break;
                        case 3:
                            Console.Clear();
                            myFunctions.getAverageAmount(myConn);
                            //int average = myFunctions.getAverageAmount(myConn);
                            //Console.WriteLine("{0}", average);
                            break;
                        case 4:
                            Console.Clear();
                            int total = myFunctions.getTotalAmount(myConn);
                            Console.WriteLine("\nTotal Amount: ${0}", total);
                            break;
                        case 5:
                            Console.Clear();
                            myFunctions.getHighLowAmount(myConn);
                            break;
                        case 6:
                            Console.WriteLine("Thank You for using the application!");
                            killswitch = 1;
                            break;
                        default:
                            killswitch = 1;
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            myConn.Close();

        }
    }
}
