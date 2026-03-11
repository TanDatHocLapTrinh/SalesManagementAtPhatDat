using Microsoft.IdentityModel.Tokens;
using SalesManagementAtPhatDat.Models;
using SalesManagementAtPhatDat.Services;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace SalesManagementAtPhatDat.Views.Staffs
{
    /// <summary>
    /// Interaction logic for UCAddStaff.xaml
    /// </summary>
    public partial class UCAddStaff : UserControl
    {
        public UCAddStaff()
        {
            InitializeComponent();
        }
        private StaffServices addStaffService = new StaffServices();
        private void btnSaveInforStaff_Click(object sender, RoutedEventArgs e)
        {
            
            if (string.IsNullOrEmpty(txtFullName.Text) ||
                string.IsNullOrEmpty(txtAddress.Text) ||
                string.IsNullOrEmpty(txtPhoneNumber.Text)
                ||string.IsNullOrEmpty(txtRole.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
                Staff staff = new Staff()
                {
                    FullName = txtFullName.Text,
                    PhoneNumber = txtPhoneNumber.Text,
                    Email = (!string.IsNullOrEmpty(txtEmail.Text)) ? txtEmail.Text : "None",
                    Address = txtAddress.Text,
                    Role = txtRole.Text,
                };
            bool result = addStaffService.AddStaff(staff);
            if (result) {
                MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtFullName.Clear();
                txtPhoneNumber.Clear();
                txtEmail.Clear();
                txtAddress.Clear();
                txtRole.Clear();

            }
            else
            {
                MessageBox.Show("Thêm không thành công!", "Thông tin", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            AdminDashboard adminDashboard = (AdminDashboard)Window.GetWindow(this);
            adminDashboard.MainContent.Content = new UCStaffManagement();
        }
    }
}
