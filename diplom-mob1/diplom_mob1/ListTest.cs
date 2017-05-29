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
            listView.ItemsSource = App.Database.GetItems();

            StackLayout stackLayout = new StackLayout
            {
                Children =
                {
                listView,

                }
            };
        }
        protected override void OnAppearing()
        {

            base.OnAppearing();
        }
    }
}