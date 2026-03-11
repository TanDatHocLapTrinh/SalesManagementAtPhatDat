using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using SalesManagementAtPhatDat.Views.Staffs;
using SalesManagementAtPhatDat.Models;
namespace SalesManagementAtPhatDat.Services
{
    internal class DatabaseService
    {
        private string connectionString = "Data Source=Database/foodapp.db";
        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }
        public void ResetID()
        {
            string resetQuery = "DELETE FROM sqlite_sequence WHERE name='Staffs'";
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(resetQuery, conn);
                cmd.ExecuteNonQuery();
            }
        }

    }
}
