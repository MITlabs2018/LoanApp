using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections;

namespace LoanApp
{
    public partial class TablePopUp : Form
    {
        string id;
       
        public TablePopUp(string var)
        {
            id = var;
            InitializeComponent();

            try
            {
                ConnectDB connection = new ConnectDB();
                MySqlConnection con = connection.setUpConnection();

                con.Open();

                string query = "SELECT * FROM debtors WHERE NIC = @id";
                
                MySqlCommand cmd3 = new MySqlCommand(query, con);
                cmd3.Parameters.AddWithValue("@id", id);
                MySqlDataReader rdr = cmd3.ExecuteReader();
                textBox1.Text = var.ToString();
                while (rdr.Read())
                {
                    txtBoxSearch.Text = rdr.GetValue(1).ToString();
                    textBox2.Text = rdr.GetValue(5).ToString();
                    textBox3.Text = rdr.GetValue(4).ToString();
   
                }

            rdr.Close();
            con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }

    }


