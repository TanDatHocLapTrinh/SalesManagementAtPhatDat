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
using SalesManagementAtPhatDat.Views.Staffs;
namespace SalesManagementAtPhatDat.Views
{
    /// <summary>
    /// Interaction logic for AdminDashboard.xaml
    /// </summary>
    public partial class AdminDashboard : Window
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }

        private void btnSignOut_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView();
            MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                loginView.Show();
                this.Close();
            }
        }

        private void btnStaff_Click(object sender, RoutedEventArgs e)
        {
            UCStaffManagement uCStaffManagement = new UCStaffManagement();
            MainContent.Content = uCStaffManagement;
        }
    }
}
