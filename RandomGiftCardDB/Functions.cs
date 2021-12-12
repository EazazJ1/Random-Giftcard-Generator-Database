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


    }
}
