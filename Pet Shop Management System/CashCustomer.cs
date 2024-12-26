using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pet_Shop_Management_System
{
    public partial class CashCustomer : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        SqlDataReader dr;
        string title = "Pet Shop Management System";
        CashForm cash;

        public CashCustomer(CashForm form)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.connection());
            cash = form;
            LoadCustomer();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadCustomer();
        }



        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCustomer.Columns[e.ColumnIndex].Name;
            if (colName == "Choice")
            {
                dbcon.executeQuery("UPDATE tbCash SET cid=" + dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString() +
                                   " WHERE transno=" + cash.lblTransno.Text + "");
                cash.loadCash();
                MessageBox.Show("Cashing sucessully by " +dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString() + ".", "Cashe For Products", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    this.Dispose();

            }

            string col_Name = dgvCustomer.Columns[e.ColumnIndex].Name;
            if (col_Name == "Billing")
            {

                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                    printDocument1.Print();
            }

        }

        #region method

        public void LoadCustomer()
        {

            try
            {
                int i = 0;
                dgvCustomer.Rows.Clear();
                cm = new SqlCommand("SELECT id,name,phone FROM tbCustomer WHERE name LIKE '%" + txtSearch.Text + "%'",
                    cn);
                cn.Open();
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgvCustomer.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
                }

                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, title);
            }
        }

        #endregion method

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Pet Shop Management System", new Font("Arial", 20, FontStyle.Bold), Brushes.Black,
                new Point(210, 20));

            e.Graphics.DrawString("=============================================================================================================================",
                new Font("Arial", 16, FontStyle.Bold), Brushes.Black,
                new Point(0, 80));


            e.Graphics.DrawString("Flower Road - Colombo 07", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(300, 60));

            e.Graphics.DrawString("Date: " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"),
                new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(80, 120));

            e.Graphics.DrawString("Transaction No: " + cash.lblTransno.Text, new Font("Arial", 12, FontStyle.Regular),
                Brushes.Black, new Point(490, 120));

            e.Graphics.DrawString("Invoice No: " + cash.lblTransno.Text, new Font("Arial", 12, FontStyle.Regular),
                Brushes.Black, new Point(490, 180));

            for (int x = 0; x < cash.dgvCash.Rows.Count; x++) { 

                 e.Graphics.DrawString("Cashier Name: " + "Mr/ Ms :" + cash.dgvCash.Rows[x].Cells[8].Value.ToString(), new Font("Arial", 12, FontStyle.Regular),
                Brushes.Black, new Point(490, 150));
            }


            for (int z = 0; z < cash.dgvCash.Rows.Count; z++)
            {

                e.Graphics.DrawString("Customer Name: " + "Mr/ Ms:" + cash.dgvCash.Rows[z].Cells[7].Value.ToString(),
                    new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(80, 150));
            }

            e.Graphics.DrawString("Sub Total: " + cash.lblTotal.Text, new Font("Arial", 12, FontStyle.Regular),
                Brushes.Black, new Point(80, 180));

            //Add selected cashier name from dataGridView






            e.Graphics.DrawString("__________________________________________________________________________________________________________________________________",
                new Font("Arial", 16, FontStyle.Bold), Brushes.Black,
                new Point(0, 205));
            
            e.Graphics.DrawString("No", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(80, 240));
            e.Graphics.DrawString("cid", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(120, 240));
            e.Graphics.DrawString("pid", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(170, 240));
            e.Graphics.DrawString("Product Name", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(220, 240));
            e.Graphics.DrawString("Qty", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(490, 240));
            e.Graphics.DrawString("Amount", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(540, 240));
            e.Graphics.DrawString("Cashier", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(690, 240));


            e.Graphics.DrawString("__________________________________________________________________________________________________________________________________",
                new Font("Arial", 16, FontStyle.Bold), Brushes.Black,
                new Point(0, 260));


            //get data from datagridview and print it to screen
            int i = 0;
            int y = 295;

            for (i = 0; i < cash.dgvCash.Rows.Count; i++)

            {
                e.Graphics.DrawString(cash.dgvCash.Rows[i].Cells[0].Value.ToString(),
                    new Font("Arial", 12, FontStyle.Regular), Brushes.Black,
                    new Point
                    (80, y));
                e.Graphics.DrawString(cash.dgvCash.Rows[i].Cells[1].Value.ToString(),
                    new Font("Arial", 12, FontStyle.Regular), Brushes.Black,
                    new
                    Point(120, y));
                e.Graphics.DrawString(cash.dgvCash.Rows[i].Cells[2].Value.ToString(),
                    new Font("Arial", 12, FontStyle.Regular), Brushes.Black,new Point
                    (170, y));
                e.Graphics.DrawString(cash.dgvCash.Rows[i].Cells[3].Value.ToString(),
                    new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point 
                    (220, y));
                e.Graphics.DrawString(cash.dgvCash.Rows[i].Cells[4].Value.ToString(),
                    new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(490, y));

                e.Graphics.DrawString(cash.dgvCash.Rows[i].Cells[5].Value.ToString(),
                    new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(540, y));
                    
                //e.Graphics.DrawString(cash.dgvCash.Rows[i].Cells[6].Value.ToString(),
                //    new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(660, y));

                //e.Graphics.DrawString(cash.dgvCash.Rows[i].Cells[7].Value.ToString(),
                //    new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(750, y));

                e.Graphics.DrawString(cash.dgvCash.Rows[i].Cells[8].Value.ToString(),
                    new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(690, y));


                //e.Graphics.DrawString(cash.dgvCash.Rows[i].Cells[9].Value.ToString(),
                //    new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(770, y));

      

                y += 30; // Increment y to move to the next line for the next row
            }

            e.Graphics.DrawString("=============================================================================================================================",
                new Font("Arial", 16, FontStyle.Bold), Brushes.Black,
                new Point(0, 1080));

            e.Graphics.DrawString("Thank you - Come Again", new Font("Arial", 18, FontStyle.Italic), Brushes.Black,
                new Point(240, 1110));


            e.Graphics.DrawString("=============================================================================================================================",
                new Font("Arial", 16, FontStyle.Bold), Brushes.Black,
                new Point(0, 1150));

        }
    }
}
    