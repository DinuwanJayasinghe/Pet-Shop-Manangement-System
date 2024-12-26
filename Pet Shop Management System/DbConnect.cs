using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Pet_Shop_Management_System
{
    class DbConnect
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        private string con;


        public string connection()
        {

            con = @"Data Source=DESKTOP-I0JJGJ1\MSSQLSERVER2022;Initial Catalog=C:\USERS\ACER\SOURCE\REPOS\PET SHOP MANAGEMENT SYSTEM\PET SHOP MANAGEMENT SYSTEM\DBPETSHOP.MDF;Integrated Security=True;Encrypt=False";
            return con;
        }
        public void executeQuery(string sql)
        {
            try
            {
                cn.ConnectionString = connection();
                cn.Open();
                cm = new SqlCommand(sql, cn);
                cm.ExecuteNonQuery();
                cn.Close();
                
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
           
        }
    }
}
