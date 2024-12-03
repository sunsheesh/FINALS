using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _045_Sorita_Telan_F1db
{
    public partial class FrmMain : Form
    {
        string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Z:\\045_Sorita_Regine\\045_Sorita_Telan_F1db\\dbPhone.mdb";
        OleDbConnection conn;
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            {
                string keyword = txtKeyword.Text;
                string query = "SELECT model_desc AS MODEL, price AS PRICE, brandid AS BRAND FROM model WHERE ";

               
                if (decimal.TryParse(keyword, out decimal priceValue))
                {
                   
                    query += "(model_desc LIKE @keyword OR price = @price OR brandid = @price)";
                }
                else if (int.TryParse(keyword, out int brandidValue))
                {
                 
                    query += "(model_desc LIKE @keyword OR brandid = @brandid)";
                }
                else
                {
                    
                    query += "model_desc LIKE @keyword";
                }

                
                conn = new OleDbConnection(connStr);
                conn.Open();

                OleDbCommand cmd = new OleDbCommand(query, conn);

                
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                
                if (decimal.TryParse(keyword, out decimal price))
                {
                    cmd.Parameters.AddWithValue("@price", price); 
                }
                else if (int.TryParse(keyword, out int brandid))
                {
                    cmd.Parameters.AddWithValue("@brandid", brandid); 
                }

              
                DataTable dt = new DataTable();
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                adapter.Fill(dt);
                conn.Close();

              
                grdResult.DataSource = dt;
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAdd add = new frmAdd();
            add.ShowDialog();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            frmEdit edit = new frmEdit();
            edit.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            frmDelete delete = new frmDelete();
            delete.ShowDialog();
        }
    }
}
