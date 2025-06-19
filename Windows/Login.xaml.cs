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

namespace r6marketplaceclient.Windows
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly MainWindow _mainWindow;
        public Login(MainWindow window)
        {
            _mainWindow = window;
            InitializeComponent();
        }

        private void whyButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "All Ubisoft API methods require authentication. You don't need to have marketplace access or own the game unless you plan to use the buy/sell features. " +
                "If you're not using those, I recommend creating an alt account to use this app instead of logging in with your main one.",
                "Why do I need to give my credentials?",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void UpdateLoginButtonState()
        {
            if (emailBox.Text.Count() > 0 && passwordBox.Password.Count() > 0) loginButton.IsEnabled = true;
            else loginButton.IsEnabled = false;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) => UpdateLoginButtonState();

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e) => UpdateLoginButtonState();

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            bool success = await ApiClient.Authenticate(emailBox.Text, passwordBox.Password);
            if (success)
            {
                _mainWindow.Show();
                Close();
                await _mainWindow.PrepareAndPerformSearch();
            }
            else
            {
                MessageBox.Show(
                    "Authentication failed. Please check your email and password.",
                    "Login Failed",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(sender is Window window && e.Key == Key.Enter)
            {
                if(loginButton.IsEnabled)
                {
                    loginButton_Click(sender, e);
                }
            }
        }
    }
}
