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
        List<String> VoprosList = new List<String>();
        List<String> TaskList = new List<String>();

        int TrueAnswer = 0, Answer = 0, TakeAnswer = 0;
        float Ocenka = 0F;
        string WebImage = "";
        int i = 7, y = 0, z = 0;
        string[] TextAnswer;
        static public bool TheTaskIs = false;

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


            AnswerTask = new Entry
            {
                IsVisible = false,
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
                    AnswerTask,
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

        public async void TakeTest()
        {
            VoprosList = await DependencyService.Get<IMySQL>().GetTakeTest();
            TaskList = await DependencyService.Get<IMySQL>().GetTakeTask();
            LabelNameVopros.Text = VoprosList[0];
            var1.Text = VoprosList[1];
            var2.Text = VoprosList[2];
            var3.Text = VoprosList[3];
            var4.Text = VoprosList[4];
            TrueAnswer = Convert.ToInt32(VoprosList[5]);
            WebImage = VoprosList[6];
            if (TaskList.Count < 2)
            {
                TheTaskIs = true;
                TextAnswer = new string[TaskList.Count / 2];
            }

        }

        private void OnButtonClickedPutTestAnswer(object sender, EventArgs e)
        {
            if (TrueAnswer == Answer)
            {
                TakeAnswer++;
            }

            else if (Answer == 0)
                LabelError.Text = "Нужно выбрать ответ!";
            else
                LabelError.Text = "Ты поиграл";


            if (VoprosList.Count > i)
            {
                LabelNameVopros.Text = VoprosList[0 + i];
                var1.Text = VoprosList[1 + i];
                var2.Text = VoprosList[2 + i];
                var3.Text = VoprosList[3 + i];
                var4.Text = VoprosList[4 + i];
                TrueAnswer = Convert.ToInt32(VoprosList[5 + i]);
                WebImage = VoprosList[6];
                i += 7;
            }
            else
            {
                btnPutTestAnswer.Text = "Решить задачу!";
                if (TaskList.Count > y && TheTaskIs)
                {
                    LabelNameVopros.Text = TaskList[0 + y];
                    WebImage = VoprosList[1 + y];
                    Variant.IsVisible = false;
                    AnswerTask.IsVisible = true;
                    y += 2;
                    TextAnswer[z] = AnswerTask.Text;
                    z++;
                }
                else
                {
                    btnPutTestAnswer.Text = "Закончить тестирование!";
                    Ocenka = (TakeAnswer * 5) / (VoprosList.Count / 7);
                    DependencyService.Get<IMySQL>().PutAnswerTest(Ocenka, TakeAnswer, TextAnswer);
                }

            }
        }
        private async void OnButtonClickedStopTestAnswer(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Registration());
        }
    }
}