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
        String debtorsID;
       
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
                    debtorsID = rdr.GetValue(0).ToString();
                    txtBoxSearch.Text = rdr.GetValue(1).ToString();
                    textBox3.Text = rdr.GetValue(4).ToString();
                    textBox2.Text = rdr.GetValue(5).ToString();
   
                }

                rdr.Close();

                string query2 = "SELECT loan.RefNo FROM loan WHERE loan.DebtorsID =@debtorsID";
                MySqlCommand cmd4 = new MySqlCommand(query2, con);
                cmd4.Parameters.AddWithValue("@debtorsID", debtorsID);
                MySqlDataReader rdr2 = cmd4.ExecuteReader();

                while (rdr2.Read())
                {
                    comboBox1.Items.Add(rdr2.GetValue(0));
                }


                rdr2.Close();
                con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

            updateTable();
        }


        private void updateTable() {
            fillTable fillTable = new fillTable();

            //String query = "SELECT loan.RefNo,loan.Amount AS Amount,installment.amount AS LastPayment,installment.Date,(loan.Amount - (SELECT SUM(installment.amount) FROM  installment WHERE installment.RefNo = loan.RefNo GROUP BY RefNo)) AS'toBePaid',loan.Status FROM loan,installment WHERE loan.RefNo = installment.RefNo AND installment.Date = (SELECT installment.Date FROM installment WHERE installment.RefNo = loan.RefNo ORDER BY installment.Date DESC LIMIT 1) AND loan.DebtorsID ="+debtorsID;

            String query = "SELECT loan.RefNo,loan.Amount AS Amount,installment.amount AS LastPayment,installment.Date,'toBePaid',loan.Status FROM loan,installment WHERE loan.RefNo = installment.RefNo AND installment.Date = (SELECT installment.Date FROM installment WHERE installment.RefNo = loan.RefNo ORDER BY installment.Date DESC LIMIT 1) AND loan.DebtorsID ="+debtorsID;
            String[] colNames = { "RefNo", "Amount", "LastPayment", "lastPaymentDate", "toBePaid", "Status" };
            fillTable.DrawTable(dataGridView1,colNames, query);
        }



        private void TablePopUp_Load(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //inserting the payment to database

            string paidAmount = textBoxAmount.Text;
            string refNum = comboBox1.Text;
            DateTime currentTime = DateTime.Now;
            dateTimePicker1.Value = currentTime;
            string date = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss");//date = dateTimePicker1.Value.ToString("yyyy-MM-dd");

            ConnectDB connection = new ConnectDB();
            MySqlConnection con = connection.setUpConnection();

            try {
                con.Open();
                string query = "INSERT INTO installment(amount,RefNo,Date) VALUES ('" + paidAmount + "','" + refNum + "','" + date + "')";
                MySqlCommand cmd = new MySqlCommand(query,con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }

            updateTable();
            label9.Text = "Updated !";
            textBoxAmount.Text= "";
            comboBox1.Text = "";
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }

    }


