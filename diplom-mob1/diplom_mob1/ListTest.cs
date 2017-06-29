using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace diplom_mob1
{
    public class ListTest : ContentPage
    {
        List<string> ListName = new List<string>();
        List<Name> List = new List<Name>();

        public ListTest()
        {
            Title = "Список тестов";

            voidTakeListName();

            for (int i = 0; (ListName.Count / 3) > i;)
            {
                List.Add(new Name() { id = Convert.ToInt32(ListName[0 + i]), name = ListName[1 + i], pdf = ListName[2 + i] });
                i += 3;
            }

            ListView listView = new ListView
            {
                HasUnevenRows = true,
                // Определяем источник данных
                ItemsSource = List,
                // Определяем формат отображения данных
                ItemTemplate = new DataTemplate(() =>
                {
                    // привязка к свойству name
                    Label nameLabel = new Label { FontSize = 25 };
                    nameLabel.SetBinding(Label.TextProperty, "name");
                    // создаем объект ViewCell.
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Orientation = StackOrientation.Vertical,
                            Children = { nameLabel }
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

            listView.ItemTapped += OnItemTapped;
            this.Content = stackLayout;
        }

        public async void voidTakeListName()
        {
            ListName = await DependencyService.Get<IMySQL>().GetTakeNameTest();
        }

        public async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            Name selectedName = e.Item as Name;
            if (selectedName != null)
            {
                Student.NameTest = selectedName.name;
                Student.idTest = selectedName.id;
                if (!String.IsNullOrEmpty(selectedName.pdf))
                {
                    bool result = await DisplayAlert("Подтвердить действие", "Вы хотите прочитать методические указания к работе?", "Да", "Нет");

                    if (result)
                        Device.OpenUri(new Uri(selectedName.pdf));
                }
                if (await DependencyService.Get<IMySQL>().CheckTestForStudent())
                    await Navigation.PushAsync(new Test());
                else
                    await DisplayAlert("Оповещение", "Вы уже прошли этот тест!", "Хорошо");
                ((ListView)sender).SelectedItem = null;
            }
            else
                ((ListView)sender).SelectedItem = null;
        }
    }
    class Name
    {
        public int id { get; set; }
        public string name { get; set; }
        public string pdf { get; set; }
    }
}
