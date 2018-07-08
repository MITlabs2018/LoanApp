using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoanApp
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            ConnectDB connection = new ConnectDB();
            MySqlConnection con = connection.setUpConnection();

            con.Open();
            MySqlCommand cmd = new MySqlCommand("select UserID,Pw from admin", con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (textBoxUsername.Text == dt.Rows[0]["UserID"].ToString() && textBoxPassword.Text == dt.Rows[0]["Pw"].ToString())
            {
                this.Visible = false;
                var form = new Form1();
                form.Visible = true;
            }
            else
            {
                MessageBox.Show("Invalid Username & Password..!!!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
