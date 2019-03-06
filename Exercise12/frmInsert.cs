using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Exercise12
{
    public partial class frmInsert : Form
    {
        private DataGridView dgvData;
        private SqlDataAdapter dataAdapter;

        public frmInsert()
        {
            InitializeComponent();
            dgvData = this.dgvInsert;
            Console.WriteLine(frmMenu.TableText);
            GetData("select * from " + frmMenu.TableText);
        }

        public void CorrectWindowSize()
        {
            int width = WinObjFunctions.CountGridWidth(dgvData);
            int height = WinObjFunctions.CountGridHeight(dgvData);
            if(width >= 800)
            {
                dgvData.Width = ClientSize.Width - 50;
                ClientSize = new Size(dgvData.Width + 25, dgvData.Height+ 20);
            } else
            {
                dgvData.Width = width;
                ClientSize = new Size(dgvData.Width + 30, dgvData.Height + 20);
            }                               
        }

        private void GetData(string selectCommand)
        {
            try
            {
                string connectionString =
                    "Data Source=SKIPHA\\SQLEXPRESS;" +
                    "Initial Catalog=DBCompany;Integrated Security=True";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    dataAdapter = new SqlDataAdapter(selectCommand, con);
                    DataTable currentTable = new DataTable();
                    dataAdapter.Fill(currentTable);
                    dgvInsert.DataSource = currentTable;
                }


                // Resize the DataGridView columns to fit the newly loaded content.
                dgvInsert.AutoResizeColumns(
                   DataGridViewAutoSizeColumnsMode.AllCells);
                dgvInsert.DataSource = (dgvInsert.DataSource as DataTable).Clone();
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
}
