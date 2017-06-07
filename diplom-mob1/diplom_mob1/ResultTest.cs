using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
//ПЕРЕДЕЛАТЬ СПИСОК РЕЗУЛЬТАТОВ!!!!
namespace diplom_mob1
{
    public class ResultTest : ContentPage
    {
        List<string> ListName = new List<string>();

        public ResultTest()
        {
            Label header = new Label
            {
                Text = "Список результатов",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            voidResultListName();

            List<Result> result = new List<Result>();
            // определяем источник данных
            for (int i = 0; ListName.Count > i;)
            {
                result.Add(new Result() { name = ListName[0 + i], ocenka = Convert.ToInt32(ListName[1 + i]) , true_quest = Convert.ToInt32(ListName[2 + i] )});
                i += 3;
            }

            ListView listView = new ListView
            {
                HasUnevenRows = true,
                // Определяем источник данных
                ItemsSource = result,

                // Определяем формат отображения данных
                ItemTemplate = new DataTemplate(() =>
                {
                    // привязка к свойству Company
                    Label nameLabel = new Label { FontSize = 20 };
                    nameLabel.SetBinding(Label.TextProperty, "name");

                    // привязка к свойству Name
                    Label ocenkaLabel = new Label { FontSize = 18 };
                    ocenkaLabel.SetBinding(Label.TextProperty, "ocenka");

                    // привязка к свойству Name
                    Label true_questLabel = new Label();
                    true_questLabel.SetBinding(Label.TextProperty, "true_quest");



                    // создаем объект ViewCell.
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Vertical,
                            Children = { nameLabel, ocenkaLabel, true_questLabel }
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

            this.Content = stackLayout;
        }


        public async void voidResultListName()
        {
            ListName = await DependencyService.Get<IMySQL>().GetResult();
        }
        class Result
        {
            public string name { get; set; }
            public float ocenka { get; set; }
            public int true_quest { get; set; }
        }
    }
}