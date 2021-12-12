﻿using System;

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
                    int test = 0;
                    bool result = int.TryParse(input, out test);
                    if(!result)
                    {
                        continue;
                    }

                    int choice = Convert.ToInt32(input);
                    
                    if (choice > 0 && choice < 7)
                    {

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
