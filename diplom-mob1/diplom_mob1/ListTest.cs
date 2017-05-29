using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace diplom_mob1
{
    public class ListTest : ContentPage
    {
        public ListTest()
        {
            var listView = new ListView();

            Label header = new Label
            {
                Text = "Список моделей",
            };


            listView.ItemsSource = App.Database.GetItems();

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
    }
}