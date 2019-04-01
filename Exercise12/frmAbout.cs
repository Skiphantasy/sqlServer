
/*
* EXERCISE............: Exercise 12.
* NAME AND LASTNAME...: Tania López Martín 
* CURSE AND GROUP.....: 2º Interface Development 
* PROJECT.............: SQL Server
* DATE................: 13 Mar 2019
*/


using Microsoft.Win32;
using System.Windows.Forms;
using FormUtilities;

namespace Exercise12
{
    public partial class frmAbout : Form
    {
        #region constructor
        public frmAbout()
        {
            InitializeComponent();
            MaximizeBox = false;
            this.CenterToParent();
            lblCounter.Text = Options.GetRegKey(@"SOFTWARE\P12", "Uses");
        }
        #endregion
    }
}
