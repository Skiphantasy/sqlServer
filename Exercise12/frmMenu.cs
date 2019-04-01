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
using FormUtilities;
using System.IO;

namespace Exercise12
{
    public partial class frmMenu : Form
    {
        static string tableText;
        public frmMenu()
        {
            InitializeComponent();
            Options.CreateRegKey(@"SOFTWARE\P12", "Uses");
            FillCombo();
            CreateLog();
        }

        public void CreateLog()
        {
            try
            {
                FileInfo[] fs;
                string savingFile = "LOG.txt";

                DirectoryInfo d = new DirectoryInfo(".");
                fs = d.GetFiles(savingFile);

                if(fs.Length < 1)
                {
                    using (StreamWriter bw = new StreamWriter(File.Open(savingFile, FileMode.Create)))
                    {
                        bw.WriteLine();
                        bw.Write("Fecha: " + System.DateTime.Now + "");
                        bw.WriteLine();
                    }

                } else
                {
                    using (StreamWriter bw = new StreamWriter(File.Open(savingFile, FileMode.Append)))
                    {
                        bw.WriteLine();
                        bw.Write("Fecha: " + System.DateTime.Now + "");
                        bw.WriteLine();
                    }
                }
            }catch (Exception)
            {
                MessageBox.Show("Error de I/O");
            }
        }

        public static string TableText { get => tableText; set => tableText = value; }

        public void FillCombo()
        {
            SqlConnection con = new SqlConnection("Data Source=SKIPHA\\SQLEXPRESS;" +
                "Initial Catalog=DBCompany;Integrated Security=True");
            con.Open();
            SqlCommand sql = new SqlCommand("SELECT * FROM DBCompany.sys.Tables", con);
            SqlDataReader reader = sql.ExecuteReader();

            while (reader.Read())
            {
                cmbTables.Items.Add(reader.GetString(0));
            }

            con.Close();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            Form form = new frmAllData();
            form.Show();
        }

        private void cmbTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            tableText = cmbTables.SelectedItem.ToString();
            Console.WriteLine(tableText);
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            Form form = new frmInsert();
            form.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Form form = new frmDelete();
            form.ShowDialog();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            Form form = new frmUpdate();
            form.ShowDialog();
        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            Form form = new frmAbout();
            form.ShowDialog();
        }
    }
}
