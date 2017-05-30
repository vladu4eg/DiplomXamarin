using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using SQLite;
using Xamarin.Forms;

namespace diplom_mob1
{
    public class ListTest : ContentPage
    {

        List<string> ListName = new List<string>();
        public ListTest()
        {
            Label header = new Label
            {
                Text = "Список тестов",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            voidTakeListName();

            List<Name> people = new List<Name>();
            // определяем источник данных
            for (int i = 0; (ListName.Count / 3) > i;)
            {
                people.Add(new Name() { id = Convert.ToInt32(ListName[0 + i]), name = ListName[1 + i], pdf = ListName[2 + i] });
                i += 3;
            }






            ListView listView = new ListView
            {
                HasUnevenRows = true,
                // Определяем источник данных
                ItemsSource = people,

                // Определяем формат отображения данных
                ItemTemplate = new DataTemplate(() =>
                {
                    // привязка к свойству Name
                    Label idLabel = new Label();
                    idLabel.SetBinding(Label.TextProperty, "id");

                    // привязка к свойству Company
                    Label nameLabel = new Label { FontSize = 18 };
                    nameLabel.SetBinding(Label.TextProperty, "name");


                    // создаем объект ViewCell.
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Vertical,
                            Children = { nameLabel, idLabel }
                        }
                    };
                })
            };

            StackLayout stackLayout = new StackLayout
            {
                Children =
                {
                header,
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
                if (!String.IsNullOrEmpty(selectedName.pdf))
                {
                    bool result = await DisplayAlert("Подтвердить действие", "Вы хотите прочитать методические указания к работе?", "Да", "Нет");
                    if (result)
                    {
                        Device.OpenUri(new Uri(selectedName.pdf));
                    }
                }
                Student.NameTest = selectedName.name;
                Student.idTest = selectedName.id;
                await Navigation.PushModalAsync(new Test());
                ((ListView)sender).SelectedItem = null;

            }
        }
    }
    class Name
    {
        public int id { get; set; }
        public string name { get; set; }
        public string pdf { get; set; }
    }
}
