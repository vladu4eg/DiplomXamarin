using Xamarin.Forms;
namespace diplom_mob1
{
    public class Student : ContentPage
    {
        StackLayout stackLayout = new StackLayout();
        Button btnTakeTest, btnTest, btnResultTest;
        static public int idTest, idStudent;
        static public string NameTest;

        public Student()
        {
            Title = "Главная страница";
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
        private void OnButtonClickedTakeTest(object sender, System.EventArgs e)
        {
             Navigation.PushAsync(new QRScanner());
        }
        private void OnButtonClickedTest(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new ListTest());
        }
        private void OnButtonClickedResultTest(object sender, System.EventArgs e)
        {
             Navigation.PushAsync(new ResultTest());
        }
    }
}