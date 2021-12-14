using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using MySql.Data;
using MySql.Data.MySqlClient;



namespace RandomGiftCardDB
{
    public class myFunctions
    {
        public static string[] getNames(MySqlConnection myConn)     //This function gets all the names from the name table in the RGCG db
        {
            string[] names = new string[10];                        //string array to store the names from the db
                
            myConn.Open();                                          //open conncetions
            string getNames = "SELECT * FROM name";                 //The query to get all the names from the db
                
            MySqlCommand getNamesCmd = new MySqlCommand(getNames, myConn);      //use the connection and call the query for getting everything from name table

            MySqlDataReader rdr = getNamesCmd.ExecuteReader();      //gets all the data from the db
            
            int count = 0;                                          //counter to iterate through the list       

            while (rdr.Read())                                      //keep reading until there is no more data coming from db
            {
                names[count] = rdr.GetString(0);                    //store the name in the local names list
                count++;
            }
            myConn.Close();                                         //close connection

            return names;                                           //return the names list
        }

        public static string[] getCardTypes(MySqlConnection myConn) //This function gets all the cardTypes from the giftcard table in the RGCG db
        {
            string[] cards = new string[10];                        //string array to store the cardTypes from the db

            myConn.Open();                                          //open conncetions
            string getCards = "SELECT * FROM giftcard";             //The query to get all the names from the db
            MySqlCommand getCardsCmd = new MySqlCommand(getCards, myConn);  //use the connection and call the query for getting everything from giftcard table

            MySqlDataReader rdr = getCardsCmd.ExecuteReader();      //gets all the data from the db
            
            int count = 0;                                          //counter to iterate through the list 

            while (rdr.Read())                                      //keep reading until there is no more data coming from db
            {
                cards[count] = rdr.GetString(0);                    //store the cardType in the local cards list
                count++;
            }
            myConn.Close();                                         //close connection

            return cards;                                           //return the cards list
        }

        public static string[] makeRandom(string[] names, string[] types)   //This function takes the names and cardTypes lists and 
        {                                                                   //makes a new random set of random data for giftcards with random names and amounts
            string[] cards = new string[60];                //list to hold 20 random giftcards, names and amounts

            int count = 0;                                  //count helps to keep track of the row that the data needs to be stored in

            var rand = new Random();                        //call the random function using rand

            for (int i = 0; i < 20; i++)                    //loop to add all 20 entries
            {
                int numName = rand.Next(0, 10);             //get random number between 1-10 for the name to choose
                Thread.Sleep(1);                            //wait to change rand value
                int numCard = rand.Next(0, 10);             //get random number between 1-10 for the cardType to choose    
                int amount = rand.Next(9, 101);             //get random number between 10-100 for the amount

                int index = count * 3;                      //index is the spot that the data needs to be stored, *3 is for making sure that its in the right row

                cards[index] = names[numName];              //save the name
                cards[index + 1] = types[numCard];          //save the cardType
                cards[index + 2] = amount.ToString();       //save the amount

                count++;
            }

            return cards;                                   //return the new list of random names, cards and amounts
        }

        public static void createRandomTable(MySqlConnection myConn)        //Function to create the RANDOM table in db
        {
            string create = "CREATE TABLE RANDOM (Name varchar(25), Type varchar(25), Amount int);";        //Query to create the RANDOM table
            MySqlCommand cmd = new MySqlCommand(create, myConn);                                            //commmand to use the connection and the query to add the RANDOM table

            myConn.Open();                  //open the connection
            cmd.ExecuteNonQuery();          //run the query
            myConn.Close();                 //close the connection    

        }

        public static void dropRandomTable(MySqlConnection myConn)      //Function to drop the RANDOM table in db
        {
            string drop = "DROP TABLE RANDOM;";                         //Query to drop the RANDOM table
            MySqlCommand cmd = new MySqlCommand(drop, myConn);          //commmand to use the connection and the query to drop the RANDOM table

            myConn.Open();                  //open the connection
            cmd.ExecuteNonQuery();          //run the query
            myConn.Close();                 //close the connection

        }

        public static void AddToRandomTable(MySqlConnection myConn, string[] randomCards)   //function to add the data to the RANDOM table
        {
            myConn.Open();
            try
            {
                string check = "SELECT * FROM random";                        //query to get all the info from random
                MySqlCommand firstcmd = new MySqlCommand(check, myConn);
                MySqlDataReader rdr = firstcmd.ExecuteReader();

                if (rdr.HasRows)     //check to see if table exist
                {
                    myConn.Close();
                    dropRandomTable(myConn);            //if it does exist then drop it
                }

                createRandomTable(myConn);          //create the RANDOM table

                int count = 0;
                for (int i = 0; i < 20; i++)        //Add all the data to the table
                {
                    int index = count * 3;

                    int amount = Convert.ToInt32(randomCards[index + 2]);

                    string insert = "INSERT INTO random VALUES(@name, @type, @amount);";         //query to add to the table
                    MySqlCommand cmd = new MySqlCommand(insert, myConn);

                    cmd.Parameters.AddWithValue("@name", randomCards[index]);               //next 3 lines are the values to add
                    cmd.Parameters.AddWithValue("@type", randomCards[index + 1]);
                    cmd.Parameters.AddWithValue("@amount", amount);

                    myConn.Open();
                    cmd.ExecuteNonQuery();
                    myConn.Close();


                    count++;
                }

            }
            catch (Exception ex)
            {
                myConn.Close();
                createRandomTable(myConn);          //create the RANDOM table

                int count = 0;
                for (int i = 0; i < 20; i++)        //Add all the data to the table
                {
                    int index = count * 3;

                    int amount = Convert.ToInt32(randomCards[index + 2]);

                    string insert = "INSERT INTO random VALUES(@name, @type, @amount);";        //query to add to the table
                    MySqlCommand cmd = new MySqlCommand(insert, myConn);

                    cmd.Parameters.AddWithValue("@name", randomCards[index]);                   //next 3 lines are the values to add
                    cmd.Parameters.AddWithValue("@type", randomCards[index + 1]);
                    cmd.Parameters.AddWithValue("@amount", amount);

                    myConn.Open();                  //open the connection
                    cmd.ExecuteNonQuery();          //run ruery
                    myConn.Close();                 //close connection


                    count++;
                }
            }
        }

        public static int getTotalAmount(MySqlConnection myConn)        //function to get the total of all giftcards
        {
            myConn.Open();
            string getTotal = "SELECT SUM(Amount) AS TotalAmount FROM random;";     //query to get the total amount
            MySqlCommand Cmd = new MySqlCommand(getTotal, myConn);                  //command to run query

            MySqlDataReader rdr = Cmd.ExecuteReader();                              //read the data returned
            
            int sum = 0;
            while (rdr.Read())      
            {
                sum = rdr.GetInt32(0);          //get the sum from db
            }

            myConn.Close();

            return sum;         //return the sum
        }

       public static void getAverageAmount(MySqlConnection myConn)      //function to get the average amount
        {
            int total = getTotalAmount(myConn);         //total for all the cards

            int average = total / 20;                   //get average

            Console.WriteLine("\nAverage Amount: ${0}", average);       //print average to screen
           
        }

        public static void getHighLowAmount(MySqlConnection myConn)     //get the highest and lowest amount
        {
            //Get the highest amount
            string[] high = new string[3];      //hold the highest amount
            myConn.Open();
            string getHigh = "SELECT * FROM random ORDER BY Amount ASC;";       //query to sort the list in ascending order, doesnt actually sort it
            MySqlCommand Cmd = new MySqlCommand(getHigh, myConn);               //command to run query
            MySqlDataReader RDR = Cmd.ExecuteReader();                          //read the data, only gets the first row

            while(RDR.Read())
            {
                high[0] = RDR.GetString(0);                                     //store the highest cards name, type and amount
                high[1] = RDR.GetString(1);
                high[2] = Convert.ToString(RDR.GetString(2));
            }

            myConn.Close();

            // Get the lowest amount
            string[] low = new string[3];        //hold the highest amount
            myConn.Open();
            string getLow = "SELECT * FROM random ORDER BY Amount DESC;";       //query to sort the list in descending order, doesnt actually sort it
            MySqlCommand CmdONE = new MySqlCommand(getLow, myConn);             //command to run query
            MySqlDataReader rdr = CmdONE.ExecuteReader();                       //read the data, only gets the first row

            while (rdr.Read())
            {
                low[0] = rdr.GetString(0);                                      //store the lowest cards name, type and amount
                low[1] = rdr.GetString(1);
                low[2] = Convert.ToString(rdr.GetString(2));
            }

            myConn.Close();

            Console.WriteLine("\nHighest: {0}, {1}, ${2}", high[0], high[1], high[2]);      //display the highest and lowest cards both lines
            Console.WriteLine("\nLowest: {0}, {1}, ${2}", low[0], low[1], low[2]);

        }

        public static void getRandomListFromDb(MySqlConnection myConn)          //get the data from the RANDOM table
        {

            myConn.Open();
            string getAll = "SELECT * FROM random;";                        //query for getting everything from the RANDOM table
            MySqlCommand CmdTwo = new MySqlCommand(getAll, myConn);         //command to ru nquery
            MySqlDataReader rdr = CmdTwo.ExecuteReader();                   //read the data

            string[] cards = new string[60];                                //hold the incoming data
            int count = 0;
            int index = 0;
            Console.ForegroundColor = ConsoleColor.Cyan;
            string columns = string.Format("{0,-12}{1,8}\t\t{2}\n", "Name", "Card Type", "Amount");                //Header for the table display
            Console.WriteLine(columns);
            Console.ForegroundColor = ConsoleColor.Gray;
            while (rdr.Read())                                                                  //keep readin guntil all the data is done
            {
                index = count * 3;

                cards[index] = rdr.GetFieldValue<string>(0);                                    //save the data in the cards list, next 3 lines
                cards[index + 1] = rdr.GetFieldValue<string>(1);
                cards[index + 2] = Convert.ToString(rdr.GetFieldValue<int>(2));

                string output = string.Format("{0,-12}{1,8}\t\t${2}", cards[index], cards[index + 1], cards[index + 2]);            //format the row of data to print to screen

                Console.WriteLine(output);          //display the row

                count++;
            }

            myConn.Close();
        }

        public static void menu()       //function for displaying the menu
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n\n---Random Gift Generator By: Eazaz Jakda---");
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.WriteLine("\n\nChoose an option to run:");
            Console.WriteLine("1. Create Random Gift Card List.");
            Console.WriteLine("2. Display Random Gift Card List.");
            Console.WriteLine("3. Average Gift Card Amount.");
            Console.WriteLine("4. Total Gift Card Amount.");
            Console.WriteLine("5. High and Low Gift Card Information.");
            Console.WriteLine("6. Exit Application.\n");
        }


    }
}
