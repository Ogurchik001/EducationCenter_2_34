using EducationCenter_2_34.Models;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        EducationCenterContext context = new EducationCenterContext();
        private int failedAttempCount;

        public AuthorizationWindow()
        {
            InitializeComponent();

            LoginTb.Text = Properties.Settings.Default.UserLogin;
            PasswordPb.Password = Properties.Settings.Default.UserPassword;
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginTb.Text))
            {
                MessageBox.Show("Все поля обязательны для заполнения.Заполните поле \"Логин\"", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (string.IsNullOrWhiteSpace(PasswordPb.Password))
            {
                MessageBox.Show("Все поля обязательны для заполнения.Заполните поле \"Пароль\"", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            User user = context.Users.FirstOrDefault(u => u.Login == LoginTb.Text && u.Password == PasswordPb.Password);

            if (user == null)
            {
                MessageBox.Show("Пользователь не найден. Проверьте правильность ввода логина или пароля", "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                failedAttempCount++;

                return;
            }

            else
            {
                CaptchaWindow captchaWindow = new CaptchaWindow();
                if (captchaWindow.ShowDialog() == true)
                {
                    {
                        MessageBox.Show("Вы успешно авторизовались", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

                        if (PasswordCb.IsChecked == true)
                        {
                            Properties.Settings.Default.UserLogin = LoginTb.Text;
                            Properties.Settings.Default.UserPassword = PasswordPb.Password;
                        }
                        else
                        {
                            Properties.Settings.Default.UserLogin = "";
                            Properties.Settings.Default.UserPassword = "";
                        }

                        Properties.Settings.Default.Save();

                        if (user.RoleId == 1)
                        {
                            CoordinatorWindow coordinateWindow = new CoordinatorWindow();
                            coordinateWindow.Show();
                            Close();
                        }
                    }
                }
                else
                {
                    {
                        MessageBox.Show("Капча собрана неверно. Попробуйте еще раз", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    failedAttempCount++; 
                    return;
                }


            }
        }
    }
}
