/*
 * EXERCISE............: Exercise 12.
 * NAME AND LASTNAME...: Tania López Martín 
 * CURSE AND GROUP.....: 2º Interface Development 
 * PROJECT.............: SQL Server
 * DATE................: 13 Mar 2019
 */


using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Exercise12
{
    public partial class frmUpdate : Form
    {
        private SqlDataAdapter dataAdapter;

        public frmUpdate()
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
                string strQuery;

                if (id.Length <= 2)
                {
                    strQuery = "SELECT " + id[0] + "ID FROM " + frmMenu.TableText;
                } else
                {
                    strQuery = "SELECT " + id[0] + "s" + id[1] + "ID FROM " + frmMenu.TableText;
                }

                command = new SqlCommand(strQuery, cnn);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    cmbUpdate.Items.Add(reader.GetValue(0) + "");
                }

                cnn.Close();
            } catch(SqlException)
            {
                MessageBox.Show("Error al realizar la consulta");
            }
        }

        private void cmbUpdate_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvUpdate.AllowUserToAddRows = false;
            string connectionSring = "Data Source=SKIPHA\\SQLEXPRESS;" +
                  "Initial Catalog=DBCompany;Integrated Security=True";
            string[] id = frmMenu.TableText.Split('s');
            string selectCommand;

            if (id.Length <= 2)
            {
                selectCommand = "SELECT * FROM " + frmMenu.TableText + " WHERE "
                + id[0] + "ID = " + cmbUpdate.SelectedItem.ToString();
            }
            else
            {
                selectCommand = "SELECT * FROM " + frmMenu.TableText + " WHERE "
                 + id[0] + "s" + id[1] + "ID = " + cmbUpdate.SelectedItem.ToString();
            }
            

            try
            {
                using (SqlConnection con = new SqlConnection(connectionSring))
                {
                    con.Open();
                    dataAdapter = new SqlDataAdapter(selectCommand, con);
                    DataTable currentTable = new DataTable();
                    dataAdapter.Fill(currentTable);
                    dgvUpdate.DataSource = currentTable;
                    con.Close();
                }

                for (int i = 0; i < dgvUpdate.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvUpdate.ColumnCount; j++)
                    {
                        if (dgvUpdate.Rows[i].Cells[j].Value.ToString().Equals(cmbUpdate.SelectedItem.ToString()))
                        {
                            dgvUpdate.Rows[i].Cells[j].ReadOnly = true;
                        }
                    }
                }

                
            }
            catch (Exception)
            {
                //MessageBox.Show("No se han podido recuperar los datos");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string sql;
            string connectionString =
                    "Data Source=SKIPHA\\SQLEXPRESS;" +
                    "Initial Catalog=DBCompany;Integrated Security=True";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand comm = new SqlCommand())
                    {
                        string[] id = frmMenu.TableText.Split('s');
                        comm.Connection = conn;
                        conn.Open();
                        int number;
                        
                        sql = "UPDATE " + frmMenu.TableText + " SET ";

                        for (int i = 0; i < dgvUpdate.Rows.Count; i++)
                        {
                            for (int j = 0; j < dgvUpdate.ColumnCount; j++)
                            {
                                if (j == (dgvUpdate.Columns.Count - 1))
                                {
                                    if (int.TryParse(dgvUpdate.Rows[i].Cells[j].Value.ToString(), out number))
                                    {                                   
                                        sql += dgvUpdate.Columns[j].HeaderText + " = "
                                        + dgvUpdate.Rows[i].Cells[j].Value;
                                    }
                                    else
                                    {
                                        sql += dgvUpdate.Columns[j].HeaderText + " = "
                                            + "'" + dgvUpdate.Rows[i].Cells[j].Value + "'";
                                    }                               
                                }
                                else
                                {
                                    if (int.TryParse(dgvUpdate.Rows[i].Cells[j].Value.ToString(), out number))
                                    {
                                    
                                        if(!dgvUpdate.Columns[j].HeaderText.ToString().Equals(id[0] + "ID"))
                                        {
                                            sql += dgvUpdate.Columns[j].HeaderText + " = "
                                            + dgvUpdate.Rows[i].Cells[j].Value + ", ";
                                        }
                                    }
                                    else
                                    {
                                        sql += dgvUpdate.Columns[j].HeaderText + " = "
                                            + "'" + dgvUpdate.Rows[i].Cells[j].Value + "'" + ", ";
                                    }
                                }
                            }
                        }

                        sql += " WHERE " + id[0] + "ID = " + cmbUpdate.SelectedItem.ToString();
                        Console.WriteLine(sql);
                        comm.CommandText = sql;
                        comm.ExecuteNonQuery();
                    }

                    MessageBox.Show("Registro actualizado correctamente");

                    try
                    {
                        using (StreamWriter bw = new StreamWriter(File.Open("LOG.txt", FileMode.Append)))
                        {
                            bw.WriteLine();
                            bw.Write("Registro actualizado en la tabla " + frmMenu.TableText);
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
                MessageBox.Show("No se ha podido actualizar el registro");

                try
                {
                    using (StreamWriter bw = new StreamWriter(File.Open("LOG.txt", FileMode.Append)))
                    {
                        bw.WriteLine();
                        bw.Write("Error al actualizar registro  en la tabla " + frmMenu.TableText);
                    }
                } catch (Exception) {
                    MessageBox.Show("Error de I/O");
                }

                this.Close();
            }
        }
    }
}
