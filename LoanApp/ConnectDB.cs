using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoanApp
{
    class ConnectDB
    {
        private MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
        private MySqlConnection connection = null;

        public MySqlConnection setUpConnection() {
        
            try
            {
                builder.Server = "localhost";
                builder.UserID = "root";
                builder.Password = "";
                builder.Database = "loanDB";
                builder.SslMode = MySqlSslMode.None;

                connection = new MySqlConnection(builder.ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }

            return connection;
        }

    }
}
