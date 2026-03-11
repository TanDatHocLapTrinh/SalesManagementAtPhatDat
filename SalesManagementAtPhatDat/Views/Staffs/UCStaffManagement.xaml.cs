using SalesManagementAtPhatDat.Models;
using SalesManagementAtPhatDat.Services;
using SalesManagementAtPhatDat.Views;
using System;
using System.Collections;
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
using System.ComponentModel;
namespace SalesManagementAtPhatDat.Views.Staffs
{
    /// <summary>
    /// Interaction logic for UCStaffManagement.xaml
    /// </summary>
    public partial class UCStaffManagement : UserControl
    {
        public UCStaffManagement()
        {
            InitializeComponent();
            LoadStaffs();
        }
        private ICollectionView _dataStaffView;
        StaffServices staffServices = new StaffServices(); 
        private void btnAddStaff_Click(object sender, RoutedEventArgs e)
        {
            AdminDashboard adminDashboard = (AdminDashboard)Window.GetWindow(this);
            adminDashboard.MainContent.Content = new UCAddStaff();
        }
        private void LoadStaffs()
        {
            dgvStaffList.ItemsSource = staffServices.GetStaff();
            _dataStaffView = CollectionViewSource.GetDefaultView(dgvStaffList.ItemsSource);
        }

        private void txtSearchPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keyWord = txtSearchPhoneNumber.Text.Trim();
            if (!string.IsNullOrEmpty(keyWord))
            {
                txtPlaceHolderSearch.Visibility = Visibility.Hidden;
                _dataStaffView.Filter = obj =>
                {
                    Staff staff = obj as Staff;
                    return staff.PhoneNumber != null && staff.PhoneNumber.Contains(keyWord);
                };
            }
            else
            {
                txtPlaceHolderSearch.Visibility = Visibility.Visible;
                _dataStaffView.Filter = null;
            }
        }

        private void btnDeleteStaff_Click(object sender, RoutedEventArgs e)
        {
            if (dgvStaffList.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn dòng để xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Staff staff = dgvStaffList.SelectedItem as Staff;

            bool result = staffServices.DeleteStaff(staff.StaffID);
            if (result)
            {
                MessageBox.Show("Đã xóa thành công!\n" + "Member: " + staff.FullName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadStaffs();
            }
        }
        private void dgvStaffList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid grid = sender as DataGrid;

            if (grid.SelectedItem != null)
            {
                Staff staff = grid.SelectedItem as Staff;

                grid.SelectedItem = null;
            }
        }
    }
}
