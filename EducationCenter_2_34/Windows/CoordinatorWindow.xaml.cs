using EducationCenter_2_34.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EducationCenter_2_34.Windows
{
    /// <summary>
    /// Логика взаимодействия для CoordinatorWindow.xaml
    /// </summary>
    public partial class CoordinatorWindow : Window
    {
       
        EducationCenterContext context = new EducationCenterContext();

        List<User> users = new List<User>();
        List<Role> roles = new List<Role>();

        public CoordinatorWindow()
        {
            InitializeComponent();

            users = context.Users
                .Include(u => u.Emplyee)
                .Include(u => u.Role)
                .ToList();
            roles = context.Roles.ToList();

            UserLv.ItemsSource = users;
            ItemsCountTbl.Text = $"Записей найдено: {users.Count}";
            LoadFilters();
        }

        private void StartFilterSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            List<User> filteredUsers = users;

            string searchText = SearchByNameTb.Text;
            int selectedRoleId = 0;

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                filteredUsers = filteredUsers.Where(u => u.Emplyee.FullName.ToLower().Contains(searchText.ToLower())).ToList();
            }

            selectedRoleId = Convert.ToInt32(FilterByRoleCmb.SelectedValue);

            if (selectedRoleId > 0)
            {
                filteredUsers = filteredUsers.Where(u => u.RoleId == selectedRoleId).ToList();
            }

            if (filteredUsers.Count > 0)
            {
                UserLv.Visibility = Visibility.Visible;
            }
            else
            {
                UserLv.Visibility = Visibility.Hidden;
            }

            ItemsCountTbl.Text = $"Записей найдено: {filteredUsers.Count}";

            UserLv.ItemsSource = filteredUsers;

        }

        private void LoadFilters()
        {
            roles.Insert(0, new Role() { Name = "Все роли" });

            FilterByRoleCmb.DisplayMemberPath = "Name";
            FilterByRoleCmb.SelectedValuePath = "Id";
            FilterByRoleCmb.ItemsSource = roles;
        }

        private void EditUserBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FullNameTb.Text) ||
                string.IsNullOrWhiteSpace(EmailTb.Text) ||
                string.IsNullOrWhiteSpace(PhoneTb.Text) ||
                string.IsNullOrWhiteSpace(NoteTb.Text) ||
                string.IsNullOrWhiteSpace(LoginTb.Text) ||
                string.IsNullOrWhiteSpace(PasswordPb.Password) ||
                HireDateDp.SelectedDate.Value == null)
                
            {
                MessageBox.Show("Все поля обяхательны для заполнения. Заполните пустые поля", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            else
            {
                Employee employee = new Employee();
                {
                    FullNameTb = FullNameTb.Text,
                    PositionId = (int)PositionCmb.SelectedValue,
                    DepartmentId = (int)DepartmentCmb.SelectedValue,
                    Email = EmailTb.Text,
                    Phone = PhoneTb.Text,
                    HireDate = DateOnly.FromDateTime(HireDateDp.SelectedDate.Value),
                    IsActive = IsActiveCb.IsChecked.Value,
                    Note = NoteTb.Text
                };
            }
        }

        private void AddUserBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}