using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Spartacus {
    //FrmException
    public partial class FE : Form {

        public FE() {
            InitializeComponent();
        }

        public FE(string title, string message) {
            InitializeComponent();
            Text = title;
            tbMessage.Text = message;
            btDetails.Visible = false;
        }

        public FE(string title, string message, string details) {
            InitializeComponent();
            Text = title;
            tbMessage.Text = message;
            tbDetails.Text = details;
        }

        public static void Show(string message, string title) {
            FE f = new FE(title, message);
            f.ShowDialog();
        }

        public static void Show(string message, string title, string details) {
            FE f = new FE(title, message, details);
            f.ShowDialog();
        }

        public static void Show(Exception ex) {
            FE f = new FE("Warning", ex.Message, ex.StackTrace);
            f.ShowDialog();
        }

        private void btClose_Click(object sender, EventArgs e) {
            Close();
        }

        private void btDetails_Click(object sender, EventArgs e) {
            if (Height < 380)
                Height = 380;
            else
                Height = 189;

        }

        private void FE_Load(object sender, EventArgs e) {

        }


    }

}