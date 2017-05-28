using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace diplom_mob1
{
    public class Teacher : ContentPage
    {
        public Teacher()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello Page Teacher" }
                }
            };
        }
    }
}