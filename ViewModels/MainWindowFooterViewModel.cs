using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace r6marketplaceclient.ViewModels
{
    public class MainWindowFooterViewModel : INotifyPropertyChanged
    {
        private bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null!)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private readonly string _username = "unknown";
        private int _balance = -1;

        public string Username
        {
            get => _username;
            init
            {
                if (SetProperty(ref _username, value))
                    OnPropertyChanged(nameof(UsernameDisplay));
            }
        }
        public int Balance
        {
            get => _balance;
            set
            {
                if(SetProperty(ref _balance, value))
                    OnPropertyChanged(nameof(BalanceDisplay));
            }
        }

        public string UsernameDisplay => $"Logged in as: {Username}";
        public string BalanceDisplay => $"Balance: {Balance}";

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
