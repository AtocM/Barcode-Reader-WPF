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
    class addSales
    {
        public void addSale(SqlConnection sqlConn, string username, DateTime saleDate, double priceNakit, double creditCard, double veresiye)
        {
            SqlCommand cmd = new SqlCommand();

            decimal priceNakitDecimal = Convert.ToDecimal(priceNakit.ToString().Replace(",","."));
            decimal creditCardDecimal = Convert.ToDecimal(creditCard.ToString().Replace(",", "."));
            decimal veresiyeDecimal = Convert.ToDecimal(veresiye.ToString().Replace(",", "."));

            cmd.Connection = sqlConn;

            cmd.CommandText = "INSERT INTO Sales(Username,Sales_Date,Total_Price,Sales_Status,CreditCard,Veresiye) Values('" + username + "','" + saleDate + "','" + priceNakitDecimal + "','1','" + creditCardDecimal + "','" + veresiyeDecimal + "')";
            
            cmd.ExecuteNonQuery();

        }
    }
}
