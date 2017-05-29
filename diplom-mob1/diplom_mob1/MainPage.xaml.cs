using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace diplom_mob1
{
    public partial class MainPage : ContentPage
    {

        Entry loginEntry, passwordEntry;
        Button btnRegistration, btnLogin;
        Label Label1, LabelError;
        static public bool AuthStudent = false, AuthTeacher = false;
        public MainPage()
        {
            Title = "Главная";
            //InitializeComponent();
            StackLayout stackLayout = new StackLayout();
            Label1 = new Label
            {
                Text = "Добро пожаловать в ад!",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            };

            loginEntry = new Entry
            {
                Placeholder = "Login",
            };

            passwordEntry = new Entry
            {
                Placeholder = "Password",
                IsPassword = true,
            };
            btnLogin = new Button
            {
                Text = "Авторизация",
            };
            btnRegistration = new Button
            {
                Text = "Регистрация",
            };
            LabelError = new Label
            {
                Text = "Анализ ошибок",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            };

            btnLogin.Clicked += OnButtonClickedLogin;
            btnRegistration.Clicked += OnButtonClickedReg;


            stackLayout.Children.Add(Label1);
            stackLayout.Children.Add(loginEntry);
            stackLayout.Children.Add(passwordEntry);
            stackLayout.Children.Add(btnLogin);
            stackLayout.Children.Add(btnRegistration);
            stackLayout.Children.Add(LabelError);
            this.Content = stackLayout;
        }
        private async void OnButtonClickedLogin(object sender, System.EventArgs e)
        {
            //не работает проверка на пустое поле
            if (loginEntry.Text != String.Empty && passwordEntry.Text != String.Empty)
                LabelError.Text = await DependencyService.Get<IMySQL>().GetAccountAuth(loginEntry.Text, passwordEntry.Text);
            else LabelError.Text = "Введите логин и пароль!";

            if (AuthStudent == true)
            {
                AuthStudent = false;
                await Navigation.PushModalAsync(new Student());
            }
            if (AuthTeacher == true)
            {
                AuthTeacher = false;
                await Navigation.PushModalAsync(new Teacher());
            }
        }
        private async void OnButtonClickedReg(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Registration());
        }
    }
}
