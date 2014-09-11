using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Windows;

namespace Barcode_Reader
{
    class callProductFromDatabase
    {
        ArrayList selectedItemArrayList = new ArrayList();
        ArrayList kurlarArrayList = new ArrayList();
        double unit_price = new double();
        int stock_amount = new int();
        string unit_trademark;
        string unit_name;
        int kdv;
        sqlString classSqlString = new sqlString();
        public ArrayList callSelectedItem(string barcode, SqlConnection sqlConnection)
        {
            selectedItemArrayList.Clear();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from Stock_information where Barcode_Number = '" + barcode + "'";
            cmd.Connection = sqlConnection;

            if(sqlConnection.State.ToString() == "Closed")
            {
                sqlConnection = classSqlString.sqlConnectionOpen();
                cmd.Connection = sqlConnection;
            }

            using (sqlConnection) 
            {
                if (sqlConnection.State.ToString() == "Closed")
                {
                    sqlConnection.Open();
                }
                SqlDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        unit_price = Convert.ToDouble(reader["Unit_Price"].ToString());
                        stock_amount = Convert.ToInt32(reader["Stock_Amount"].ToString());
                        unit_trademark = reader["Unit_trademark"].ToString();
                        unit_name = reader["Unit_Name"].ToString();
                        kdv = Convert.ToInt32(reader["KDV"].ToString()); ;

                        selectedItemArrayList.Add(unit_price);
                        selectedItemArrayList.Add(stock_amount);
                        selectedItemArrayList.Add(unit_trademark);
                        selectedItemArrayList.Add(unit_name);
                        selectedItemArrayList.Add(kdv);

                    }
                }
                catch
                {

                }
            }

            return selectedItemArrayList;
        }
        public ArrayList kurlar(SqlConnection sqlConnection)
        {
            double dollar;
            double euro;
            double pound;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM kurlar";
            cmd.Connection = sqlConnection;
            if (sqlConnection.State.ToString() == "Closed")
            {
                sqlConnection = classSqlString.sqlConnectionOpen();
                cmd.Connection = sqlConnection;
            }

            using (sqlConnection)
            {
                if (sqlConnection.State.ToString() == "Closed")
                {
                    sqlConnection.Open();
                }
                SqlDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        dollar = Convert.ToDouble(reader["dolar"].ToString());
                        euro = Convert.ToDouble(reader["euro"].ToString());
                        pound = Convert.ToDouble(reader["pound"].ToString());

                        kurlarArrayList.Add(dollar);
                        kurlarArrayList.Add(euro);
                        kurlarArrayList.Add(pound);

                    }
                }
                catch
                {

                }
            }

            return kurlarArrayList;
        }
    }
}
