using System.Text.RegularExpressions;

namespace TecnologicoApp.Views
{
    public partial class MainPage : ContentPage
    {
        Entry emailEntry;
        Entry passwordEntry;
        Button loginButton;

        public MainPage()
        {
            InitializeComponent();

            emailEntry = new Entry { Placeholder = "Correo electrónico" };
            passwordEntry = new Entry { Placeholder = "Contraseña", IsPassword = true };
            loginButton = new Button { Text = "Iniciar Sesión" };
            loginButton.Clicked += OnLoginButtonClicked;

            Content = new StackLayout
            {
                Margin = new Thickness(20),
                Children =
                {
                    emailEntry,
                    passwordEntry,
                    loginButton
                }
            };
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(emailEntry.Text) || !IsValidEmail(emailEntry.Text))
            {
                await DisplayAlert("Error", "Ingrese un correo electrónico válido.", "Aceptar");
                return;
            }

            if (string.IsNullOrWhiteSpace(passwordEntry.Text) || !IsValidPassword(passwordEntry.Text))
            {
                await DisplayAlert("Error", "Ingrese una contraseña válida. Debe contener al menos una letra, un número y un símbolo.", "Aceptar");
                return;
            }

            await DisplayAlert("Éxito", "Inicio de sesión exitoso.", "Aceptar");
        }

        bool IsValidEmail(string email)
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

        bool IsValidPassword(string password)
        {
            return Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
        }
    }
}
