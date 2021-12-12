using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data;
using MySql.Data.MySqlClient;



namespace RandomGiftCardDB
{
    public class myFunctions
    {
        public static string[] getNames(MySqlConnection myConn)
        {
            string[] names = new string[10];

            myConn.Open();
            string getNames = "SELECT * FROM name";
            MySqlCommand getNamesCmd = new MySqlCommand(getNames, myConn);

            MySqlDataReader rdr = getNamesCmd.ExecuteReader();
            int count = 0;

            while (rdr.Read())
            {
                names[count] = rdr.GetString(0);
                count++;
            }
            myConn.Close();

            return names;
        }

        public static string[] getCardTypes(MySqlConnection myConn)
        {
            string[] cards = new string[10];

            myConn.Open();
            string getCards = "SELECT * FROM giftcard";
            MySqlCommand getCardsCmd = new MySqlCommand(getCards, myConn);

            MySqlDataReader rdr = getCardsCmd.ExecuteReader();
            int count = 0;

            while (rdr.Read())
            {
                cards[count] = rdr.GetString(0);
                count++;
            }
            myConn.Close();

            return cards;
        }

        public static string[] makeRandom(string[] names, string[] types)
        {
            string[] cards = new string[60];

            int count = 0;

            var rand = new Random();

            for (int i = 0; i < 20; i++)
            {
                int num = rand.Next(0, 10);
                int price = rand.Next(9, 101);

                int index = count * 3;

                cards[index] = names[num];
                cards[index + 1] = types[num];
                cards[index + 2] = price.ToString();

                count++;
            }

            return cards;
        }

        public static void createRandomTable(MySqlConnection myConn)
        {
            string create = "CREATE TABLE RANDOM (Name varchar(25), Type varchar(25), Amount int);";
            MySqlCommand cmd = new MySqlCommand(create, myConn);

            myConn.Open();
            cmd.ExecuteNonQuery();
            myConn.Close();

        }

        public static void dropRandomTable(MySqlConnection myConn)
        {
            string drop = "DROP TABLE RANDOM;";
            MySqlCommand cmd = new MySqlCommand(drop, myConn);

            myConn.Open();
            cmd.ExecuteNonQuery();
            myConn.Close();

        }

        public static void AddToRandomTable(MySqlConnection myConn, string[] randomCards)
        {
            myConn.Open();

            string check = "SELECT * FROM name";
            MySqlCommand firstcmd = new MySqlCommand(check, myConn);
            MySqlDataReader rdr = firstcmd.ExecuteReader();

            if(rdr.HasRows)     //check to see if table exist
            {
                myConn.Close();
                dropRandomTable(myConn);            //if it does exist then drop it
            }

            createRandomTable(myConn);          //create the RANDOM table

            int count = 0;
            for (int i = 0; i < 20; i++)        //Add all the 
            {
                int index = count * 3;

                int amount = Convert.ToInt32(randomCards[index + 2]);

                string insert = "INSERT INTO random VALUES(@name, @type, @amount);";
                MySqlCommand cmd = new MySqlCommand(insert, myConn);

                cmd.Parameters.AddWithValue("@name", randomCards[index]);
                cmd.Parameters.AddWithValue("@type", randomCards[index + 1]);
                cmd.Parameters.AddWithValue("@amount", amount);

                myConn.Open();
                cmd.ExecuteNonQuery();
                myConn.Close();


                count++;
            }

        }

        public static int getTotalAmount(MySqlConnection myConn)
        {
            //string[] names = new string[10];

            myConn.Open();
            string getTotal = "SELECT SUM(Amount) AS TotalAmount FROM random;";
            MySqlCommand Cmd = new MySqlCommand(getTotal, myConn);

            MySqlDataReader rdr = Cmd.ExecuteReader();
            //int count = 0;
            int sum = 0;
            while (rdr.Read())
            {
                //names[count] = rdr.GetString(0);
                //count++;
                sum = rdr.GetInt32(0);
            }
            Console.WriteLine("{0}", sum);
            myConn.Close();

            return sum;
        }

       public static int getAverageAmount(MySqlConnection myConn)
        {
            int total = getTotalAmount(myConn);

            int average = total / 20;

            return average;

        }

        public static void getHighLowAmount(MySqlConnection myConn)
        {
            //string[] names = new string[10];
            string[] high = new string[3];
            myConn.Open();
            string getTotal = "SELECT * FROM random ORDER BY Amount ASC;";
            MySqlCommand Cmd = new MySqlCommand(getTotal, myConn);
            MySqlDataReader RDR = Cmd.ExecuteReader();

            while(RDR.Read())
            {
                high[0] = RDR.GetString(0);
                high[1] = RDR.GetString(1);
                high[2] = Convert.ToString(RDR.GetString(2));
            }

            myConn.Close();

            //string[] names = new string[10];
            string[] low = new string[3];
            myConn.Open();
            string getLow = "SELECT * FROM random ORDER BY Amount DESC;";
            MySqlCommand CmdONE = new MySqlCommand(getLow, myConn);
            MySqlDataReader rdr = CmdONE.ExecuteReader();

            while (rdr.Read())
            {
                low[0] = rdr.GetString(0);
                low[1] = rdr.GetString(1);
                low[2] = Convert.ToString(rdr.GetString(2));
            }

            myConn.Close();

            Console.WriteLine("{0}, {1}, {2}", high[0], high[1], high[2]);
            Console.WriteLine("{0}, {1}, {2}", low[0], low[1], low[2]);


            //myConn.Open();
            //string getAll = "SELECT * FROM random;";
            //MySqlCommand CmdTwo = new MySqlCommand(getAll, myConn);
            //MySqlDataReader rdr = CmdTwo.ExecuteReader();

            //string[] cards = new string[60];
            //int count = 0;
            //int index = 0;
            //while (rdr.Read())
            //{
            //    index = count * 3;

            //    cards[index] = rdr.GetFieldValue<string>(0);
            //    cards[index + 1] = rdr.GetFieldValue<string>(1);
            //    cards[index + 2] = Convert.ToString(rdr.GetFieldValue<int>(2));


            //    count++;
            //}

            //for(int i = 0; i < 60; i++)
            //{
            //    Console.WriteLine("{0}", cards[i]);
            //}

            //myConn.Close();


            //return sum;
        }



    }
}
