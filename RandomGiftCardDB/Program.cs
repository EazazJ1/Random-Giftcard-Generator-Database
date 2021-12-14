using System;

using MySql.Data;
using MySql.Data.MySqlClient;



namespace RandomGiftCardDB
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] names = new string[10];        //strings to hold the names table, cardType table and the new data that needs to be added to db
            string[] giftCards = new string[10];
            string[] randomCards = new string[60];

            string myDb = "server=localhost;userid=application;password=Database1234;database=rgcg";        //the string with info that is needed to access the db
            MySqlConnection myConn = new MySqlConnection(myDb);     //create the connection

            try
            {
                names = myFunctions.getNames(myConn);           //get the names table

                giftCards = myFunctions.getCardTypes(myConn);       //get the cardTypes tables               

                int killswitch = 0;         //condition to exit while loop

                while(killswitch == 0)
                {
                    myFunctions.menu();     //display menu

                    string input = Console.ReadLine();
                    int test = 0;
                    bool result = int.TryParse(input, out test);    //input validation
                    
                    if(!result)     //if not number then display menu again
                    {
                        continue;
                    }

                    int choice = Convert.ToInt32(input);    
                    
                    if (choice > 0 && choice < 7)       //if input is right then run program
                    {

                        switch (choice)
                        {
                            case 1:     //create random gift card list
                                Console.Clear();
                                randomCards = myFunctions.makeRandom(names, giftCards);     //create the list of the random giftcards to be added to db
                                myFunctions.AddToRandomTable(myConn, randomCards);
                                break;
                            case 2:     //Display Random Gift Card List
                                Console.Clear();
                                myFunctions.getRandomListFromDb(myConn);
                                break;
                            case 3:     //Average Gift Card Amount
                                Console.Clear();
                                myFunctions.getAverageAmount(myConn);
                                break;
                            case 4:     //Total Gift Card Amount.
                                Console.Clear();
                                int total = myFunctions.getTotalAmount(myConn);
                                Console.WriteLine("\nTotal Amount: ${0}", total);
                                break;
                            case 5:     //High and Low Gift Card Information
                                Console.Clear();
                                myFunctions.getHighLowAmount(myConn);
                                break;
                            case 6:     //Exit Application
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\nThank You for using the Random Gift Card Genrator application!");
                                Console.ForegroundColor = ConsoleColor.Gray;
                                killswitch = 1;
                                break;
                            default:
                                killswitch = 1;
                                break;
                        }
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
