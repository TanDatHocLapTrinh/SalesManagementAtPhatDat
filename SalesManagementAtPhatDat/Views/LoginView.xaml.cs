using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SQLite;
using Microsoft.Data.Sqlite;
using SalesManagementAtPhatDat.Models;
using SalesManagementAtPhatDat.Helpers;

namespace SalesManagementAtPhatDat.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }
        public User CheckLogin(string username, string password)
        {
            string connStr = "Data Source=Database/foodapp.db";

            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string hashedPassword = SecurityHelper.HashPassword(password);
                string query = "SELECT Username, Role FROM Users WHERE UserName=@u AND Password=@p";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.Parameters.AddWithValue("@p", password);

                    SQLiteDataReader reader = cmd.ExecuteReader();
                    if (reader.Read()) {
                        return new User
                        {
                            UserName = reader["Username"].ToString(),
                            Role = reader["Role"].ToString()
                        };
                    }
                }
                return null;
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUserName.Text;
            string password = pwbPassword.Password;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng tài khoản và mật khẩu!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                LoginView vm = new LoginView();
                User user = vm.CheckLogin(username, password);
                if (user != null)
                {
                    if (user.Role == "Admin")
                    {
                        AdminDashboard admin = new AdminDashboard();
                        admin.Show();
                    }
                    else
                    {
                        StaffDashboard staff = new StaffDashboard();
                        staff.Show();
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
