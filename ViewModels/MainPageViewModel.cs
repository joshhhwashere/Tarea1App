using Microsoft.Maui.ApplicationModel.Communication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TecnologicoApp.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public ICommand LoginCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageViewModel()
        {
            LoginCommand = new Command(OnLogin);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnLogin()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                DisplayAlert("Error", "Please enter an email.", "OK");
                return;
            }

            if (!IsValidEmail(Email))
            {
                DisplayAlert("Error", "Please enter a valid email address.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                DisplayAlert("Error", "Please enter a password.", "OK");
                return;
            }

            if (!IsValidPassword(Password))
            {
                DisplayAlert("Error", "Password must contain at least one letter, one number, and one symbol.", "OK");
                return;
            }

            // Aquí iría la lógica para iniciar sesión
            DisplayAlert("Success", "Login successful!", "OK");
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPassword(string password)
        {
            return Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
        }

        private async void DisplayAlert(string title, string message, string button)
        {
            await App.Current.MainPage.DisplayAlert(title, message, button);
        }
    }
}