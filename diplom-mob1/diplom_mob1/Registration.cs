using Xamarin.Forms;

namespace diplom_mob1
{
    public class Registration : ContentPage
    {
        SwitchCell var1;
        Button btnReg;
        TableView RegMenu;
        Label ErrorLabel;
        EntryCell Login, pass1, pass2, LastName, FirstName, MiddleName, Group, zachetka;

        public Registration()
        {
            Title = "Регистрация";
            btnReg = new Button
            {
                Text = "Зарегистрироваться!",
            };
            ErrorLabel = new Label
            {
                TextColor = Color.Red,
                FontSize = 20
            };
            RegMenu = new TableView
            {
                Intent = TableIntent.Menu,
                Root = new TableRoot
                {
                new TableSection ("Регистрация")
                    {
                    (var1 = new SwitchCell { Text = "Вы преподаватель?" } ),
                    (Login = new EntryCell { Label = "Логин:"} ),
                    (pass1 = new EntryCell { Label = "Пароль:"} ),
                    (pass2 = new EntryCell { Label = "Пароль:", Placeholder = "Еще раз" } ),
                    (LastName = new EntryCell { Label = "Фамилия:"} ),
                    (FirstName = new EntryCell { Label = "Имя:"} ),
                    (MiddleName = new EntryCell { Label = "Отчество:"} ),
                    (Group = new EntryCell { Label = "Группа:"} ),
                    (zachetka = new EntryCell { Label = "Номер зачетки:"} ),
                    (new ViewCell { View = btnReg }),
                    (new ViewCell { View = ErrorLabel}),
                    }
                }
            };

            var1.OnChanged += (s, e) =>
            {
                if (var1.On == true)
                {
                    Group.IsEnabled = false;
                    zachetka.IsEnabled = false;
                    Group.Text = "";
                    zachetka.Text = "";
                }
                else
                {
                    Group.IsEnabled = true;
                    zachetka.IsEnabled = true;
                }
            };

            btnReg.Clicked += OnButtonClickedLogin;
            this.Content = RegMenu;
        }
        private async void OnButtonClickedLogin(object sender, System.EventArgs e)
        {
            if (pass1.Text != pass2.Text)
                ErrorLabel.Text = "Пароль не совпадает!";
            else if (string.IsNullOrEmpty(Login.Text))
                  ErrorLabel.Text = "Введите Логин";
            else if (string.IsNullOrEmpty(FirstName.Text))
                ErrorLabel.Text = "Введите Имя";
            else if (string.IsNullOrEmpty(LastName.Text))
                ErrorLabel.Text = "Введите Фамилию";
            else if (string.IsNullOrEmpty(MiddleName.Text))
                ErrorLabel.Text = "Введите Отчество";
            else if (string.IsNullOrEmpty(Group.Text) && var1.On == false)
                ErrorLabel.Text = "Введите Группу";
            else if (string.IsNullOrEmpty(zachetka.Text) && var1.On == false)
                ErrorLabel.Text = "Введите номер зачетки";
            else
            {
                if (var1.On == false && await DependencyService.Get<IMySQL>().GetAccountReg(Login.Text, pass1.Text, LastName.Text, FirstName.Text, MiddleName.Text, Group.Text, zachetka.Text))
                {
                    await DisplayAlert("Оповещение", "Вы зарегистрировали учетную запись СТУДЕНТ.", "Хорошо");
                }
                else if (var1.On = true && await DependencyService.Get<IMySQL>().GetAccountReg(Login.Text, pass1.Text, LastName.Text, FirstName.Text, MiddleName.Text))
                {
                    await DisplayAlert("Оповещение", "Вы зарегистрировали учетную запись ПРЕПОДАВАТЕЛЬ.", "Хорошо");
                }
                else
                    await DisplayAlert("Оповещение", "Регистрация не прошла", "Хорошо");
            }
        }
    }
}