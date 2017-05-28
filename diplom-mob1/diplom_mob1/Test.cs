using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Diagnostics;

using Xamarin.Forms;

namespace diplom_mob1
{
    public class Test : ContentPage
    {
        Entry AnswerTask;
        Button btnPutTestAnswer, btnStopTestAnswer;
        Label LabelNameVopros, LabelError, LabelNameTest;
        SwitchCell var1, var2, var3, var4;
        TableView Variant;
        List<String> products = new List<String>();

        int TrueAnswer = 0, Answer = 0;
        string WebImage = "";
        int i = 7;

        //

        public Test()
        {
            LabelNameTest = new Label
            {
                Text = "Название вопроса",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            };

            LabelNameVopros = new Label
            {
                Text = "Название вопроса",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            };

            LabelError = new Label
            {
                Text = "Анализ ошибок",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            };

            btnPutTestAnswer = new Button
            {
                Text = "След вопрос",
            };

            btnStopTestAnswer = new Button
            {
                Text = "Закончить тестирование",
            };


            Variant = new TableView
            {
                HasUnevenRows = true,
                Intent = TableIntent.Menu,
                Root = new TableRoot
                {
                new TableSection ("Выберите один из ответов")
                    {
                    (var1 = new SwitchCell {  }),
                    (var2 = new SwitchCell {  }),
                    (var3 = new SwitchCell {  }),
                    (var4 = new SwitchCell {  })
                    }
                }

            };

            var webImage = new Image { Aspect = Aspect.AspectFit };
            webImage.Source = ImageSource.FromUri(new Uri("https://xamarin.com/content/images/pages/forms/example-app.png"));

            var1.OnChanged += (s, e) =>
            {
                if (var1.On == true)
                {
                    var2.On = false;
                    var3.On = false;
                    var4.On = false;
                    Answer = 1;
                }
            };
            var2.OnChanged += (s, e) =>
            {
                if (var2.On == true)
                {
                    var1.On = false;
                    var3.On = false;
                    var4.On = false;
                    Answer = 2;
                }
            };
            var3.OnChanged += (s, e) =>
            {

                if (var3.On == true)
                {
                    var1.On = false;
                    var2.On = false;
                    var4.On = false;
                    Answer = 3;
                }
            };
            var4.OnChanged += (s, e) =>
            {
                if (var4.On == true)
                {
                    var1.On = false;
                    var2.On = false;
                    var3.On = false;
                    Answer = 4;
                }
            };

            // var scroll = new ScrollView();
            //  Content = scroll;

            StackLayout stackLayout = new StackLayout
            {
                Children =
                {
                    LabelNameTest,
                    LabelNameVopros,
                    webImage,
                    Variant,
                    btnPutTestAnswer,
                    btnStopTestAnswer,
                    LabelError,

                }
            };


            ScrollView scrollView = new ScrollView();
            scrollView.Content = stackLayout;
            this.Content = scrollView;


            TakeTest();

            btnPutTestAnswer.Clicked += OnButtonClickedPutTestAnswer;
            btnStopTestAnswer.Clicked += OnButtonClickedStopTestAnswer;
        }

        private async void TakeTest()
        {
            products = await DependencyService.Get<IMySQL>().GetTakeTest();
            LabelNameVopros.Text = products[0];
            var1.Text = products[1];
            var2.Text = products[2];
            var3.Text = products[3];
            var4.Text = products[4];
            TrueAnswer = Convert.ToInt32(products[5]);
            WebImage = products[6];
        }

        private void OnButtonClickedPutTestAnswer(object sender, EventArgs e)
        {
            if (TrueAnswer == Answer)
                LabelError.Text = "Ты выиграл миллион";
            else if (Answer == 0)
                LabelError.Text = "Нужно выбрать ответ!";
            else
                LabelError.Text = "Ты поиграл";

            if (products.Count > i)
            {
                LabelNameVopros.Text = products[0 + i];
                var1.Text = products[1 + i];
                var2.Text = products[2 + i];
                var3.Text = products[3 + i];
                var4.Text = products[4 + i];
                TrueAnswer = Convert.ToInt32(products[5 + i]);
                WebImage = products[6];
                i += 7;
            }
        }
        private async void OnButtonClickedStopTestAnswer(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Registration());
        }
    }
}