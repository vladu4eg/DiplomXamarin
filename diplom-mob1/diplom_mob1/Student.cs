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
        static public int idTest, idStudent;
        static public string NameTest;

        public Student()
        {
            StackLayout stackLayout = new StackLayout();

            btnTakeTest = new Button
            {
                Text = "Получить тест",
            };
            btnTest = new Button
            {
                Text = "Выбрать тест для прохождения",
            };
            btnResultTest = new Button
            {
                Text = "Результаты тестов",
            };


            btnTakeTest.Clicked += OnButtonClickedTakeTest;
            btnTest.Clicked += OnButtonClickedTest;
            btnResultTest.Clicked += OnButtonClickedResultTest;

            stackLayout.Children.Add(btnTakeTest);
            stackLayout.Children.Add(btnTest);
            stackLayout.Children.Add(btnResultTest);

            this.Content = stackLayout;
        }
        private async void OnButtonClickedTakeTest(object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new QRScanner());
        }
        private async void OnButtonClickedTest(object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new ListTest());
        }
        private async void OnButtonClickedResultTest(object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new ResultTest());
        }
    }
}