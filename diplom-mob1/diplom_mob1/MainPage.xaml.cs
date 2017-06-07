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

        Entry passwordEntry, loginEntry;
        Button btnRegistration, btnLogin;
        static public bool AuthStudent = false, AuthTeacher = false;
        public MainPage()
        {
            //BackgroundImage = "BG-SJ.jpg";

            btnLogin = new Button
            {
                Text = "Авторизация",
            };
            btnRegistration = new Button
            {
                Text = "Регистрация",
            };

            loginEntry = new Entry
            {
                Placeholder = "Введите логин",
                Keyboard = Keyboard.Default,

            };
            passwordEntry = new Entry
            {
                Placeholder = "Введите пароль",
                Keyboard = Keyboard.Default,
                IsPassword = true,

            };

            TableView Menu = new TableView
            {
                Intent = TableIntent.Form,
                Root = new TableRoot
                {
                new TableSection ("Авторизуйтесь для прохождения тестирования.")
                    {
                    (new ViewCell
                    {
                        View = loginEntry
                    }),
                    (new ViewCell
                    {
                        View = passwordEntry
                    }),
                    (new ViewCell
                    {
                        View = btnLogin
                    }),
                    (new ViewCell
                    {
                        View = btnRegistration
                    }),

                    }
                }

            };

            this.Content = Menu;


            btnLogin.Clicked += OnButtonClickedLogin;
            btnRegistration.Clicked += OnButtonClickedReg;

        }
        private async void OnButtonClickedLogin(object sender, System.EventArgs e)
        {
            //не работает проверка на пустое поле
            if (!String.IsNullOrEmpty(loginEntry.Text) && !String.IsNullOrEmpty(passwordEntry.Text))
                await DisplayAlert("Оповещение", await DependencyService.Get<IMySQL>().GetAccountAuth(loginEntry.Text, passwordEntry.Text), "Хорошо");
            else
                await DisplayAlert("Оповещение", "Введите логин и пароль!", "Хорошо");

            if (AuthStudent == true)
            {
                AuthStudent = false;
                await Navigation.PushModalAsync(new Student());
            }
            if (AuthTeacher == true)
            {
                AuthTeacher = false;
                await DisplayAlert("Оповещение", "Вы пытаетесь зайти как преподаватель! Зайдите в свою учетную запись через PC версию программы.", "Хорошо");
            }
        }

        private async void OnButtonClickedReg(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Registration());
        }
    }
}
