using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoanApp
{
    class fillTable
    {
        MySqlConnection con;
        ConnectDB newcon = new ConnectDB();

        public void DrawTable(DataGridView MyDGV, String[] Columns, String MysqlQuery)
        {
            con = newcon.setUpConnection();

            con.Open();

            String Mysqlstatement = MysqlQuery;
            MySqlCommand cmd1 = new MySqlCommand(Mysqlstatement, con);
            MySqlDataReader rdr = cmd1.ExecuteReader();

            MyDGV.Rows.Clear();

            while (rdr.Read())
            {
                DataTable dt = new DataTable();

                foreach (String col_Name in Columns)
                {
                    dt.Columns.Add(col_Name);
                }

                DataRow row = dt.NewRow();

                int count = 0;
                foreach (String col_Name in Columns)
                {

                    if (count < Columns.Length)
                    {
                        row[col_Name] = rdr.GetValue(count);
                    }

                    count++;
                }

                dt.Rows.Add(row);

                foreach (DataRow Drow in dt.Rows)
                {
                    int num = MyDGV.Rows.Add(row);
                    int countnew = 0;
                    foreach (String col_Name in Columns)
                    {
                        if (countnew > Columns.Length)
                            break;
                        MyDGV.Rows[num].Cells[countnew].Value = Drow[col_Name].ToString();
                        countnew++;
                    }
                }
            }
            rdr.Close();
            con.Close();
        }
    }
}

