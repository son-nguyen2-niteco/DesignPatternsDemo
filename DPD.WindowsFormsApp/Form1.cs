using System;
using System.Windows.Forms;

namespace DPD.WindowsFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void doTwoThingsButton_Click1(object sender, EventArgs e)
        {
            MessageBox.Show("I observe the event first!");
        }

        private void doTwoThingsButton_Click2(object sender, EventArgs e)
        {
            MessageBox.Show("Ok fine! I observe the event a bit later :)");
        }
    }
}