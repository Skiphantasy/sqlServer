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
using System.Windows.Forms;
using System.IO;

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
               
                dgvInsert.AutoResizeColumns(
                   DataGridViewAutoSizeColumnsMode.AllCells);
                dgvInsert.DataSource = (dgvInsert.DataSource as DataTable).Clone();
            }
            catch (SqlException)
            {
                MessageBox.Show("Error al conectar a la base de datos");
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string StrQuery;
            string connectionString =
                    "Data Source=SKIPHA\\SQLEXPRESS;" +
                    "Initial Catalog=DBCompany;Integrated Security=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand comm = new SqlCommand())
                    {
                        comm.Connection = conn;
                        conn.Open();
                        int number;
                        
                        StrQuery = @" SET IDENTITY_INSERT " + frmMenu.TableText + " ON INSERT INTO " + frmMenu.TableText  + " (";

                        for (int j = 0; j < dgvData.Columns.Count; j++)
                        {
                            if( j == (dgvData.Columns.Count - 1))
                            {
                                StrQuery += dgvData.Columns[j].HeaderText + ")";
                            } else
                            {
                                StrQuery += dgvData.Columns[j].HeaderText + ", ";
                            }

                        }

                        StrQuery += " VALUES (";

                        for (int i = 0; i < dgvData.Rows.Count; i++)
                        {

                            for (int j = 0; j < dgvData.Columns.Count; j++)
                            {
                                if (dgvData.Rows[i].Cells[j].Value != null)
                                {
                                    Console.WriteLine(dgvData.Rows[i].Cells[j].Value);

                                    if (j == (dgvData.Columns.Count - 1))
                                    {
                                        if (int.TryParse(dgvData.Rows[i].Cells[j].Value.ToString(), out number))
                                        {
                                            StrQuery += dgvData.Rows[i].Cells[j].Value + ")";
                                        }
                                        else
                                        {
                                            StrQuery += "'" + dgvData.Rows[i].Cells[j].Value + "'";
                                        }

                                    }
                                    else
                                    {
                                        if (int.TryParse(dgvData.Rows[i].Cells[j].Value.ToString(), out number))
                                        {
                                            StrQuery += dgvData.Rows[i].Cells[j].Value + ", ";
                                        }
                                        else
                                        {
                                            StrQuery += "'" + dgvData.Rows[i].Cells[j].Value + "', ";
                                        }
                                    }
                                }
                            }
                        }

                        StrQuery += ") SET IDENTITY_INSERT " + frmMenu.TableText + " OFF;";
                        Console.WriteLine(StrQuery);
                        comm.CommandText = StrQuery;
                        comm.ExecuteNonQuery();
                    }

                    MessageBox.Show("Registro insertado correctamente");
                    try
                    {
                        using (StreamWriter bw = new StreamWriter(File.Open("LOG.txt", FileMode.Append)))
                        {
                            bw.WriteLine();
                            bw.Write("Registro insertado en la tabla " + frmMenu.TableText);
                        }

                    } catch(Exception)
                    {
                        MessageBox.Show("Error de I/O");
                    }

                    this.Close();
                }
            }
            catch (Exception)
            {
                try
                {
                    MessageBox.Show("No se ha podido insertar el registro");
                    using (StreamWriter bw = new StreamWriter(File.Open("LOG.txt", FileMode.Append)))
                    {
                        bw.WriteLine();
                        bw.Write("Error al insertar registro en la tabla " + frmMenu.TableText);
                    }
                } catch(Exception)
                {
                    MessageBox.Show("Error de I/O");
                }

                this.Close();
            }
        }
    }
}
