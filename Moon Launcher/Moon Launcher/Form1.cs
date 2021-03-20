using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moon_Launcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "MoonFN.Dev" && txtPassword.Text == "Moon.dev")
            {
                MessageBox.Show("Welcome To MoonFN! This is the Pre-release so it means you Are an Tester, Admin, or Developer. If not please don't send it to friends.", "Welcome!");
                this.Hide();
                new Launcher().Show();
            }


            else
            {
                MessageBox.Show("Hold Up! you tried to Login with a Wrong Username or Password. Try it again, Or Contact Us on Discord that we can Help you.", "MoonFN Login System");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
