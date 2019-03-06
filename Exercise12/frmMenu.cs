using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Exercise12
{
    public partial class frmMenu : Form
    {
        static string tableText;
        public frmMenu()
        {
            InitializeComponent();
            FillCombo();
        }

        public static string TableText { get => tableText; set => tableText = value; }

        public void FillCombo()
        {
            SqlConnection con = new SqlConnection("Data Source=SKIPHA\\SQLEXPRESS;" +
                "Initial Catalog=DBCompany;Integrated Security=True");
            con.Open();
            SqlCommand sql = new SqlCommand("SELECT * FROM DBCompany.sys.Tables", con);
            SqlDataReader reader = sql.ExecuteReader();
            while(reader.Read())
            {
                cmbTables.Items.Add(reader.GetString(0));
                //Console.WriteLine(reader.GetString(0));
            }
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
            form.Show();
        }
    }
}
