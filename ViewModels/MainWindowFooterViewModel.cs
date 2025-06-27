using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace r6marketplaceclient.ViewModels
{
    public class MainWindowFooterViewModel : INotifyPropertyChanged
    {
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null!)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private string _username = "unknown";
        private int _balance = -1;

        public string Username
        {
            get => _username;
            set
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
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
