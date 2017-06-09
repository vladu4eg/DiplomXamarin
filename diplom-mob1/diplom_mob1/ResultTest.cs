using System;
using System.Collections.Generic;
using Xamarin.Forms;
namespace diplom_mob1
{
    public class ResultTest : ContentPage
    {
        List<string> ListName = new List<string>();

        public ResultTest()
        {
            Title = "Список результатов";

            Label namet = new Label
            {
                Text = "Название темы:",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            };
            Label tq = new Label
            {
                Text = "Правильные ответы:",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            };
            Label fq = new Label
            {
                Text = "Неправильные ответы:",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            };
            Label ocenkalab = new Label
            {
                Text = "Оценка:",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            };

            voidResultListName();

            List<Result> result = new List<Result>();
            for (int i = 0; ListName.Count > i;)
            {
                result.Add(new Result() { name = ListName[0 + i], false_quest = Convert.ToInt32(ListName[1 + i]) , true_quest = Convert.ToInt32(ListName[2 + i]), ocenka = Convert.ToInt32(ListName[3 + i] ) });
                i += 4;
            }

            ListView listView = new ListView
            {
                HasUnevenRows = true,
                SeparatorColor = Color.Red,
                ItemsSource = result,
                // Определяем формат отображения данных
                ItemTemplate = new DataTemplate(() =>
                {
                    // привязка к свойству name
                    Label nameLabel = new Label { FontSize = 25, TextColor = Color.Blue};
                    nameLabel.SetBinding(Label.TextProperty, "name");
                    // привязка к свойству false_quest
                    Label false_quest = new Label { FontSize = 25, TextColor = Color.Blue };
                    false_quest.SetBinding(Label.TextProperty, "false_quest");
                    // привязка к свойству true_quest
                    Label true_questLabel = new Label { FontSize = 25, TextColor = Color.Blue };
                    true_questLabel.SetBinding(Label.TextProperty, "true_quest");
                    // привязка к свойству ocenka
                    Label ocenkaLabel = new Label { FontSize = 25, TextColor = Color.Blue };
                    ocenkaLabel.SetBinding(Label.TextProperty, "ocenka");
                    // создаем объект ViewCell.
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Vertical,
                            Children = { namet, nameLabel, fq, false_quest, tq, true_questLabel, ocenkalab, ocenkaLabel }
                        }
                    };
                })
            };

            StackLayout stackLayout = new StackLayout
            {
                Children =
                {
                listView,             
                }
            };
            this.Content = stackLayout;
            listView.ItemTapped += OnItemTapped;
        }

        public async void voidResultListName()
        {
            ListName = await DependencyService.Get<IMySQL>().GetResult();
        }

        public void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
                ((ListView)sender).SelectedItem = null;
        }

        class Result
        {
            public string name { get; set; }
            public int false_quest { get; set; }
            public int true_quest { get; set; }
            public int ocenka { get; set; }
        }
    }
}