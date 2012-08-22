#region Using directives

using System;
using System.Collections.Generic;
using System.Windows.Forms;

#endregion

namespace Spartacus
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
//            string t = Fcn.Decimal2BinaryStr(8);
            Application.EnableVisualStyles();
            bool runAsExperiment = false;
            //runAsExperiment = true;
            if (runAsExperiment) {
            //if (args.Length > 0) {
                Application.Run(new FrmConsole());
            } else {
                FrmSplash fs = new FrmSplash();
                fs.ShowDialog();
                Application.Run(new FrmMain());
            }
        }

    }
}