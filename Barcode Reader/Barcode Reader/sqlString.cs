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
    class sqlString
    {
        string sql = @"Data Source=AYKUT\SQLEXPRESS;Initial Catalog=kilincBR;Integrated Security=True;";
        public string sqlStringSend()
        {
            return sql;
        }
        public SqlConnection sqlConnectionOpen()
        {
            SqlConnection connectToDatabase = new SqlConnection();
            connectToDatabase.ConnectionString = sql;
            try
            {
                connectToDatabase.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veritabanı ile bağlantı kurulamadığı için uygulama kapatılacak.." + ex.Message.ToString());
            }
            return connectToDatabase;
        }
        public void sqlConnectionClose(SqlConnection connection)
        {
            try
            {
                connection.Close();
            }
            catch
            {
                MessageBox.Show("Bağlantı kapatılamadı");
            }

        }

    }
}
