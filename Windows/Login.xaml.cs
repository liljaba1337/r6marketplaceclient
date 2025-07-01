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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using r6marketplaceclient.UserControls;

namespace r6marketplaceclient.Windows
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly MainWindow _mainWindow;
        private readonly SearchUserControl _searchUserControl;
        public Login(MainWindow window, SearchUserControl searchUserControl)
        {
            _searchUserControl = searchUserControl;
            _mainWindow = window;
            InitializeComponent();
            loginButton.IsEnabled = false;
        }

        private void whyButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "All Ubisoft API methods require authentication. You don't need to have marketplace access or own the game unless you plan to use the buy/sell features. " +
                "If you're not using those, I recommend creating an alt account to use this app instead of logging in with your main one.\n\n" +
                "Your credentials are stored in data/secret.dat using Windows encryption, so no one without direct access to your PC can decode them. " +
                "Sensitive data is wiped from memory right after use. The data class implements IDisposable and clears all private fields immediately. " +
                "You can verify this yourself in SecureStorage.cs to be 100% sure that your information stays safe and private.",
                "Why do I need to give my credentials?",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void UpdateLoginButtonState()
        {
            if (emailBox.Text.Count() > 0 && passwordBox.Password.Count() > 0)
            {
                loginButton.IsEnabled = true;
                loginButton.Effect = new DropShadowEffect
                {
                    Color = Color.FromRgb(0, 255, 249),
                    BlurRadius = 20,
                    ShadowDepth = 0,
                    Opacity = 0.7
                };
            }
            else
            {
                loginButton.IsEnabled = false;
                loginButton.Effect = null;
            }
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
                await _searchUserControl.PrepareAndPerformSearch();
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
