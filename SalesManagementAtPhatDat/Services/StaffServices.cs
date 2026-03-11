using SalesManagementAtPhatDat.Models;
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
    internal class StaffServices
    {
        /// <summary>
        ///  Create new database service
        ///  Connect to data source SQLite
        /// </summary>
        private DatabaseService databaseService = new DatabaseService();
        private string connectionString = "Data Source=Database/foodapp.db";

        /// <summary>
        /// Add new staff in Database SQLite
        /// </summary>
        /// <param name="staff">The object contain the information that needs to be added (FullName, PhoneNumber, Email, Address, Role).</param>
        /// <returns>
        /// Return <c>true</c> if added succesfully(At least one line has been inserted.); 
        /// otherwise return<c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method use <see cref="SQLiteCommand"/> to excute the INSERT query
        /// and automatically manage connections through the block <c>using</c>
        public bool AddStaff(Staff staff)
        {
            databaseService.GetConnection();
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO Staffs
                                 (FullName, PhoneNumber, Email, Address, Role)
                                 VALUES
                                 (@name,@phone,@email,@address,@role)";
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", staff.FullName);
                cmd.Parameters.AddWithValue("@phone", staff.PhoneNumber);
                cmd.Parameters.AddWithValue("@email", staff.Email);
                cmd.Parameters.AddWithValue("@address", staff.Address);
                cmd.Parameters.AddWithValue("@role", staff.Role);
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }
        
        /// <summary>
        /// Delete staff in Database SQLite
        /// </summary>
        /// <param name="staffID">The input parameter is ID of staff</param>
        /// <returns> Return <c>True</c> if staff was deleted
        /// otherwise return <c>False</c> 
        /// </returns>
        /// <remarks>Use query search ID of staff to perform this method</remarks>
        public bool DeleteStaff(int staffID)
        {
            databaseService.GetConnection();
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = @"DELETE FROM Staffs WHERE StaffID = @staffID";
                
                SQLiteCommand cmd = new SQLiteCommand(query, conn);

                cmd.Parameters.AddWithValue("@staffID", staffID);

                int result = cmd.ExecuteNonQuery();
                databaseService.ResetID();
                return result > 0;
            }
            
        }

        /// <summary>
        /// Get full name, phone number, email,... into list
        /// </summary>
        /// <returns> Return <c>staffs list</c> </returns>
        /// <remarks>Use <see cref="SQLiteDataReader"/>to read all information from database<remarks>
        public List<Staff> GetStaff()
        {
            List<Staff> staffs = new List<Staff>();
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Staffs";
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    staffs.Add(new Staff()
                    {
                        StaffID = reader.GetInt32(0),
                        FullName = reader["FullName"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        Email = reader["Email"].ToString(),
                        Address = reader["Address"].ToString(),
                        Status = int.Parse(reader["Status"].ToString()),
                        Role = reader["Role"].ToString(),
                    });
                }
            }
            return staffs;
        }
    }
}
