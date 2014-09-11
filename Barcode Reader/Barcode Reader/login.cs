using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows;
namespace Barcode_Reader
{
    class login
    {
        static int userID;
        static string usernameGlobal;
        public bool returnLoginState(SqlConnection sqlConnection,string username,string password)
        {
            int checkedState = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandText = "Select User_ID From User_Information Where Username ='" + username + "' And Password = '" + password + "'";
            checkedState = Convert.ToInt32(cmd.ExecuteScalar());
            usernameGlobal = username;

            userID = checkedState;

            if( checkedState > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int returnID()
        {
            return userID;
        }
        public string returnUsername()
        {
            return usernameGlobal;
        }
    }
}
