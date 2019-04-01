/*
 * EXERCISE............: Exercise 12.
 * NAME AND LASTNAME...: Tania López Martín 
 * CURSE AND GROUP.....: 2º Interface Development 
 * PROJECT.............: SQL Server
 * DATE................: 13 Mar 2019
 */


using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Exercise12
{
    public partial class frmAllData : Form
    {
        private DataGridView dgvData;
        private SqlDataAdapter dataAdapter;

        public frmAllData()
        {
            InitializeComponent();
            dgvData = this.dgvInfo;            
            Console.WriteLine(frmMenu.TableText);
            GetData("select * from " + frmMenu.TableText);
        }

        public void CorrectWindowSize()
        {
            int width = WinObjFunctions.CountGridWidth(dgvData);
            int height = WinObjFunctions.CountGridHeight(dgvData);            

            if(width >= 800 && height >= 800)
            {
                ClientSize = new Size(ClientSize.Width, ClientSize.Height);
            }
            else if( width < 800 && height >= 800)
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
                              
                /*dgvInfo.AutoResizeColumns(
                   DataGridViewAutoSizeColumnsMode.AllCells);*/
                //CorrectWindowSize();
                dgvData.AllowUserToAddRows = false;

                try
                {
                    using (StreamWriter bw = new StreamWriter(File.Open("LOG.txt", FileMode.Append)))
                    {
                        bw.WriteLine();
                        bw.Write("Se ha realizado una consulta sobre la tabla " + frmMenu.TableText);

                    }
                } catch(Exception)
                {
                    MessageBox.Show("Error de I/O");
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Error al recuperar los datos");
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
