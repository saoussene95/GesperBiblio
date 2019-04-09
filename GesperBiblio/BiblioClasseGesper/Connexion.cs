using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace GesperLibrary
{
    public class Connexion
    {
        static public MySqlConnection cnx;
        static public string sCnx = "user=root;password=siojjr;host=localhost;database=gesper";
        static Connexion()
        {
            cnx = new MySqlConnection(sCnx);
        }
        static public MySqlConnection GetCnx
        {
            get
            {
                return cnx;
            }
        }
    }
}
