using Xamarin.Forms;
namespace diplom_mob1
{
    public class Student : ContentPage
    {
        StackLayout stackLayout = new StackLayout();
        Button btnTakeTest, btnTest, btnResultTest, btnExit;
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
            btnExit = new Button
            {
                Text = "Выход",
            };

            btnTakeTest.Clicked += OnButtonClickedTakeTest;
            btnTest.Clicked += OnButtonClickedTest;
            btnResultTest.Clicked += OnButtonClickedResultTest;
            btnExit.Clicked += OnButtonClickedExit;
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
        private async void OnButtonClickedExit(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}