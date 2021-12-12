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
            string[] cards = new string[65];

            int count = 0;

            var rand = new Random();

            for(int i = 0; i < 20; i++)
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


    }
}
