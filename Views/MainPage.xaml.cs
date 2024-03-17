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

            // Crear controles
            emailEntry = new Entry { Placeholder = "Correo electrónico" };
            passwordEntry = new Entry { Placeholder = "Contraseña", IsPassword = true };
            loginButton = new Button { Text = "Iniciar Sesión" };
            loginButton.Clicked += OnLoginButtonClicked;

            // Agregar controles al diseño
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
            // Validar correo electrónico
            if (string.IsNullOrWhiteSpace(emailEntry.Text) || !IsValidEmail(emailEntry.Text))
            {
                await DisplayAlert("Error", "Ingrese un correo electrónico válido.", "Aceptar");
                return;
            }

            // Validar contraseña
            if (string.IsNullOrWhiteSpace(passwordEntry.Text) || !IsValidPassword(passwordEntry.Text))
            {
                await DisplayAlert("Error", "Ingrese una contraseña válida. Debe contener al menos una letra, un número y un símbolo.", "Aceptar");
                return;
            }

            // Lógica para iniciar sesión
            // Aquí puedes agregar la lógica para iniciar sesión con el correo electrónico y la contraseña ingresados
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
            // Validar que la contraseña tenga al menos una letra, un número y un símbolo
            return Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
        }
    }
}