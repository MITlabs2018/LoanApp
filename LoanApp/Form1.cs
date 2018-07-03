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

namespace LoanApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            hideAllPanels();
            panelHome.Visible = true;

        }

        private void MessagesLabel_Click(object sender, EventArgs e)
        {

        }


        private String firstName = null;
        private String lastName = null;
        private String nic = null;
        private String phoneNum = null;
        private String address = null;

        private String amount = null;
        private String rate = null;
        private String method = null;
        private String calFreq = null;
        private String date = null;
        private int refNo = 0;
        private int debtorsID = 0;

        private void nextBtn_Click(object sender, EventArgs e)
        {
            panelAddNewDebtor.Hide();
            panelAddLoanDetails.Show();
            AddDebtorLbl.Hide();
            AddloanLbl.Show();

            firstName = txtBoxFName.Text;
            lastName = txtBoxLname.Text;
            nic = txtBoxNIC.Text;
            String[] phone = txtBoxPhone.Text.Split('-');
            phoneNum = Regex.Replace((phone[0] + phone[1] + phone[2]),@"\s","");
            address = txtBoxAddress.Text;

            cmbBoxMethod.SelectedIndex = 0;
            cmbBoxCalculatingfreq.SelectedIndex = 0;

            try
            {
                ConnectDB connection = new ConnectDB();
                MySqlConnection con = connection.setUpConnection();

                con.Open();

                String MysqlStatement_3 = "SELECT MAX(loan.RefNo) FROM loan";
                MySqlCommand cmd3 = new MySqlCommand(MysqlStatement_3, con);
                MySqlDataReader rdr = cmd3.ExecuteReader();

                while (rdr.Read())
                {
                    refNo = (int)rdr.GetValue(0) + 1;

                }

                refNoLbl.Text = refNo.ToString();

                rdr.Close();
                con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            panelAddNewDebtor.Show();
            panelAddLoanDetails.Hide();
            AddDebtorLbl.Show();
            AddloanLbl.Hide();

            
        }

        private void addDebtor()
        {
            amount = txtBoxLoanAmount.Text;
            rate = txtBoxInterest.Text;
            method = cmbBoxMethod.Text;
            calFreq = cmbBoxCalculatingfreq.Text;

            date = dateTimePicker1.Value.ToString("yyyy-MM-dd");//2018-05-31 00:00:00  2018/05/31 00:00:00 PM


            ConnectDB connection = new ConnectDB();
            MySqlConnection con = connection.setUpConnection();
            try
            {
                con.Open();

                String Mysqlstatement_1 = "INSERT INTO debtors(FName,LName,NIC,Phone,Address) VALUES ('" + firstName + "','" + lastName + "','" + nic + "','" + phoneNum + "','" + address + "')";
                MySqlCommand cmd1 = new MySqlCommand(Mysqlstatement_1, con);
                cmd1.ExecuteNonQuery();

                //need to find the debtorsID of the last debtor///////////////////////////////////

                String MysqlStatement_2 = "SELECT MAX(debtors.DebtorID) FROM debtors";
                MySqlCommand cmd2 = new MySqlCommand(MysqlStatement_2, con);
                MySqlDataReader rdr = cmd2.ExecuteReader();

                while (rdr.Read())
                {
                    debtorsID =  (int)rdr.GetValue(0);
                }

                rdr.Close();
               
                String MysqlStatement_1 = "INSERT INTO loan(Amount,BorrowedDate,Interest,Status,CalculatingFrequency,Method,DebtorsID) VALUES ('" + amount + "','" + date + "','" + rate + "','0','" + calFreq + "','" + method + "','" + debtorsID + "'); INSERT INTO installment (amount,RefNo,Date) VALUES ('0', '"+ refNo + "', '"+ dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm") + "');"; //2018 - 05 - 31 00:00:00
                MessageBox.Show(MysqlStatement_1);
                MySqlCommand cmd = new MySqlCommand(MysqlStatement_1,con);
                cmd.ExecuteNonQuery();
                con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            try {
                addDebtor();
                MessageBox.Show("Debtor Profile is Created Successfully !");
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }




        private void btnDebtorList_Click(object sender, EventArgs e)
        {
            hideAllPanels();
            panelDebtorsList.Visible = true;
            DebtorListLbl.Show();

            btnDebtorList.BackColor = Color.DarkCyan;
            btnDebtorList.ForeColor = Color.White;

            cmbBoxSearchBy.SelectedIndex = 0;

            String[] colArray = { "First_Name","Last_Name","NIC_","Current_Debt","BorrowedDate", "LastPayment"};
            String query = "SELECT debtors.FName,debtors.LName,debtors.NIC,(loan.Amount - Sum(installment.amount)),loan.BorrowedDate,installment.amount FROM debtors,loan,installment WHERE debtors.DebtorID = loan.DebtorsID AND installment.RefNo = loan.RefNo GROUP By loan.RefNo";
            fillTable fillTableObj = new fillTable();
            fillTableObj.DrawTable(dataGridView1,colArray, query);
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            hideAllPanels();
            panelReport.Visible = true;
            ReportLbl.Show();

            btnReports.BackColor = Color.DarkCyan;
            btnReports.ForeColor = Color.White;
        }

        private void btnAddNewDebtor_Click_1(object sender, EventArgs e)
        {
            hideAllPanels();
            panelAddNewDebtor.Visible = true;
            AddDebtorLbl.Show();

            btnAddNewDebtor.BackColor = Color.DarkCyan;
            btnAddNewDebtor.ForeColor = Color.White;
        }

        private void hideAllPanels() {
            AddDebtorLbl.Hide();
            AddloanLbl.Hide();
            DebtorListLbl.Hide();
            ReportLbl.Hide();

            btnReports.BackColor = DefaultBackColor;
            btnReports.ForeColor = DefaultForeColor;

            btnDebtorList.BackColor = DefaultBackColor;
            btnDebtorList.ForeColor = DefaultForeColor;

            btnAddNewDebtor.BackColor = DefaultBackColor;
            btnAddNewDebtor.ForeColor = DefaultForeColor;

            panelDebtorsList.Visible = false;
            panelAddNewDebtor.Visible = false;
            panelHome.Visible = false;
            panelReport.Visible = false;
            panelAddLoanDetails.Visible = false;
        }

        private void searchDB(String SearchBy) {

            if (SearchBy == "First Name")
            {
                String[] colArray = { "First_Name", "Last_Name", "NIC_", "Current_Debt", "BorrowedDate", "LastPayment" };
                String query = "SELECT debtors.FName,debtors.LName,debtors.NIC,(loan.Amount - Sum(installment.amount)),loan.BorrowedDate,installment.amount FROM debtors,loan,installment WHERE debtors.DebtorID = loan.DebtorsID AND installment.RefNo = loan.RefNo AND debtors.FName LIKE '%"+ txtBoxSearch.Text+ "%' GROUP By loan.RefNo";
                fillTable fillTableObj = new fillTable();
                fillTableObj.DrawTable(dataGridView1, colArray, query);
                

            }

            if (SearchBy == "Last Name")
            {
                String[] colArray = { "First_Name", "Last_Name", "NIC_", "Current_Debt", "BorrowedDate", "LastPayment" };
                String query = "SELECT debtors.FName,debtors.LName,debtors.NIC,(loan.Amount - Sum(installment.amount)),loan.BorrowedDate,installment.amount FROM debtors,loan,installment WHERE debtors.DebtorID = loan.DebtorsID AND installment.RefNo = loan.RefNo AND debtors.LName LIKE '%" + txtBoxSearch.Text + "%' GROUP By loan.RefNo";
                fillTable fillTableObj = new fillTable();
                fillTableObj.DrawTable(dataGridView1, colArray, query);
            }

            if (SearchBy == "NIC")
            {
                String[] colArray = { "First_Name", "Last_Name", "NIC_", "Current_Debt", "BorrowedDate", "LastPayment" };
                String query = "SELECT debtors.FName,debtors.LName,debtors.NIC,(loan.Amount - Sum(installment.amount)),loan.BorrowedDate,installment.amount FROM debtors,loan,installment WHERE debtors.DebtorID = loan.DebtorsID AND installment.RefNo = loan.RefNo AND debtors.NIC LIKE '%" + txtBoxSearch.Text + "%' GROUP By loan.RefNo";
                fillTable fillTableObj = new fillTable();
                fillTableObj.DrawTable(dataGridView1, colArray, query);
            }

            if (SearchBy == "Date")
            {
                String[] colArray = { "First_Name", "Last_Name", "NIC_", "Current_Debt", "BorrowedDate", "LastPayment" };
                String query = "SELECT debtors.FName,debtors.LName,debtors.NIC,(loan.Amount - Sum(installment.amount)),loan.BorrowedDate,installment.amount FROM debtors,loan,installment WHERE debtors.DebtorID = loan.DebtorsID AND installment.RefNo = loan.RefNo AND loan.BorrowedDate LIKE '%" + txtBoxSearch.Text + "%' GROUP By loan.RefNo";
                fillTable fillTableObj = new fillTable();
                fillTableObj.DrawTable(dataGridView1, colArray, query);
            }


        }

        private void txtBoxSearch_TextChanged(object sender, EventArgs e)
        {
            searchDB((String)cmbBoxSearchBy.SelectedItem);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string variable = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            
            TablePopUp form = new TablePopUp(variable);
            form.Show(this);
            form.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() +" "+dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            


        }
    }

    
   
}
