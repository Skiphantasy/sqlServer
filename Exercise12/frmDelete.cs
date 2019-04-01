/*
 * EXERCISE............: Exercise 12.
 * NAME AND LASTNAME...: Tania López Martín 
 * CURSE AND GROUP.....: 2º Interface Development 
 * PROJECT.............: SQL Server
 * DATE................: 13 Mar 2019
 */


using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Exercise12
{
    public partial class frmDelete : Form
    {
        public frmDelete()
        {
            InitializeComponent();
            ComboQuery();
        }

        private void ComboQuery()
        {
            try
            {
                SqlConnection cnn;
                string connetionString = "Data Source=SKIPHA\\SQLEXPRESS;" +
                        "Initial Catalog=DBCompany;Integrated Security=True";
                cnn = new SqlConnection(connetionString);
                cnn.Open();
                SqlCommand command;
                SqlDataReader reader;
                string[] id = frmMenu.TableText.Split('s');
                string strQuery = "SELECT " + id[0] + "ID FROM " + frmMenu.TableText;
                command = new SqlCommand(strQuery, cnn);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    cmbDelete.Items.Add(reader.GetValue(0) + "");
                }
            } catch (SqlException)
            {
                MessageBox.Show("Error al consultar la base de datos");
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string[] id = frmMenu.TableText.Split('s');
            string StrQuery = "DELETE FROM " + frmMenu.TableText + " WHERE " + id[0] + "ID = " + cmbDelete.SelectedItem.ToString();
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
                        comm.CommandText = StrQuery;
                        comm.ExecuteNonQuery();
                    }

                    MessageBox.Show("Registro borrado correctamente");

                    try
                    {
                        using (StreamWriter bw = new StreamWriter(File.Open("LOG.txt", FileMode.Append)))
                        {
                            bw.WriteLine();
                            bw.Write("Registro borrado en la tabla " + frmMenu.TableText);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error de I/O");
                    }
                    this.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No se ha podido borrar el registro");

                try
                {
                    using (StreamWriter bw = new StreamWriter(File.Open("LOG.txt", FileMode.Append)))
                    {
                        bw.WriteLine();
                        bw.Write("Error al borrar registro en la tabla " + frmMenu.TableText);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error de I/O");
                }

                this.Close();
            }
        }
    }
}
