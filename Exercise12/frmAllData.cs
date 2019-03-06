using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exercise12
{
    public partial class frmAllData : Form
    {
        private DataGridView dgvData;
        //private BindingSource bindingSource = new BindingSource();
        private SqlDataAdapter dataAdapter;
        public frmAllData()
        {
            InitializeComponent();
            dgvData = this.dgvInfo;
            //bindingSource = this.dBCompanyDataSetBindingSource;
            Console.WriteLine(frmMenu.TableText);
            //dgvData.DataSource = bindingSource;
            GetData("select * from " + frmMenu.TableText);
        }

        public void CorrectWindowSize()
        {
            int width = WinObjFunctions.CountGridWidth(dgvData);
            int height = WinObjFunctions.CountGridHeight(dgvData);            

            if(width >= 800 && height >= 800)
            {
                //dgvData.Width = width;
                //dgvData.Height = height;
                ClientSize = new Size(ClientSize.Width, ClientSize.Height);
                //ClientSize = new Size(ClientSize.Width, dgvData.Height + 80);
            } else if( width < 800 && height >= 800)
            {
                dgvData.Width = width;
                ClientSize = new Size(dgvData.Width + 60, ClientSize.Height);
            }
            else if(width >= 800 && height < 800)
            {
                dgvData.Height = height;
                ClientSize = new Size(ClientSize.Width, dgvData.Height + 80);
            }else
            {
                dgvData.Width = width;
                dgvData.Height = height;
                ClientSize = new Size(dgvData.Width + 60, dgvData.Height + 80);
            }
        }

        private void GetData(string selectCommand)
        {
            try
            {       
                string connectionString =
                    "Data Source=SKIPHA\\SQLEXPRESS;" +
                    "Initial Catalog=DBCompany;Integrated Security=True";
                
                using(SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    dataAdapter = new SqlDataAdapter(selectCommand, con);
                    DataTable currentTable = new DataTable();
                    dataAdapter.Fill(currentTable);
                    dgvInfo.DataSource = currentTable;
                }
                              

                // Resize the DataGridView columns to fit the newly loaded content.
                dgvInfo.AutoResizeColumns(
                   DataGridViewAutoSizeColumnsMode.AllCells);
                CorrectWindowSize();
            }
            catch (SqlException)
            {
                MessageBox.Show("To run this example, replace the value of the " +
                    "connectionString variable with a connection string that is " +
                    "valid for your system.");
            }
        }
    }

    public static class WinObjFunctions
    {
        public static int CountGridWidth(DataGridView dgv)
        {
            int width = 0;
            foreach (DataGridViewColumn column in dgv.Columns)
                if (column.Visible == true)
                    width += column.Width;
            return width += 20;
        }
        public static int CountGridHeight(DataGridView dgv)
        {
            int height = 0;
            foreach (DataGridViewRow row in dgv.Rows)
                if (row.Visible == true)
                    height += row.Height;
            return height += 20;
        }
    }
}
