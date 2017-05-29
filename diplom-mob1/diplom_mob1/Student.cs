using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace diplom_mob1
{
    public class Student : ContentPage
    {
        Button btnTakeTest, btnTest, btnResultTest;
        Label LabelError;
        static public int idTest, idStudent;

        public Student()
        {
            StackLayout stackLayout = new StackLayout();

            btnTakeTest = new Button
            {
                Text = "Получить тест",
            };
            btnTest = new Button
            {
                Text = "Решить тест",
            };
            btnResultTest = new Button
            {
                Text = "Результаты тестов",
            };
            LabelError = new Label
            {
                Text = "Анализ ошибок",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            };

            btnTakeTest.Clicked += OnButtonClickedTakeTest;
            btnTest.Clicked += OnButtonClickedTest;
            btnResultTest.Clicked += OnButtonClickedResultTest;

            stackLayout.Children.Add(btnTakeTest);
            stackLayout.Children.Add(btnTest);
            stackLayout.Children.Add(btnResultTest);
            stackLayout.Children.Add(LabelError);

            this.Content = stackLayout;
        }
        private async void OnButtonClickedTakeTest(object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new QRScanner());
        }
        private async void OnButtonClickedTest(object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new Test());
        }
        private async void OnButtonClickedResultTest(object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new ListTest());
        }
    }
}