/*
 * EXERCISE............: Exercise 12.
 * NAME AND LASTNAME...: Tania López Martín 
 * CURSE AND GROUP.....: 2º Interface Development 
 * PROJECT.............: SQL Server
 * DATE................: 13 Mar 2019
 */


using System;
using System.Windows.Forms;

namespace Exercise12
{
    static class MainClass
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            frmSplashScreen splash;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            splash = new frmSplashScreen();

            if (splash.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new frmMenu());
            }
        }
    }
}
