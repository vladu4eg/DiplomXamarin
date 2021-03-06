﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;
namespace diplom_mob1
{
    public class Test : ContentPage
    {
        Entry AnswerTask;
        Button btnPutTestAnswer;
        Label LabelNameVopros, LabelNameTest;
        SwitchCell var1, var2, var3, var4;
        TableView Variant;
        Image webImage;

        List<String> VoprosList = new List<String>();
        List<String> TaskList = new List<String>();

        int TrueAnswer = 0, FalseAnswer = 0, Answer = 0, TakeAnswer = 0;
        int i = 7, y = 0, z = 0;
        string[] TextAnswer;
        static public bool TheTaskIs = false;
        bool AnswerIt = true;

        public Test()
        {
            Title = "";
            LabelNameTest = new Label
            {
                Text = Student.NameTest,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            };

            LabelNameVopros = new Label
            {
                Text = "Название вопроса",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            };

            btnPutTestAnswer = new Button
            {
                Text = "След вопрос",
            };

            AnswerTask = new Entry
            {
                IsVisible = false,
            };

            Variant = new TableView
            {
                Intent = TableIntent.Menu,
                Root = new TableRoot
                {
                new TableSection ("Выберите один из ответов")
                    {
                    (var1 = new SwitchCell {  }),
                    (var2 = new SwitchCell {  }),
                    (var3 = new SwitchCell { }),
                    (var4 = new SwitchCell {  }),

                    }
                }

            };
            webImage = new Image { Aspect = Aspect.AspectFit };
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

                }
            };

            ScrollView scrollView = new ScrollView();
            scrollView.Content = stackLayout;
            this.Content = stackLayout;

            TakeTest();
            btnPutTestAnswer.Clicked += OnButtonClickedPutTestAnswer;
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
            if (!String.IsNullOrEmpty(VoprosList[6]))
                webImage.Source = ImageSource.FromUri(new Uri(VoprosList[6]));
            else
                webImage.Source = ImageSource.FromUri(new Uri("http://imtis.ru/no.jpg"));
            if (TaskList.Count > 2)
            {
                TheTaskIs = true;
                TextAnswer = new string[TaskList.Count / 2];
            }
        }

        private async void OnButtonClickedPutTestAnswer(object sender, EventArgs e)
        {

            if (VoprosList.Count > i && AnswerIt)
            {
                if (TrueAnswer == Answer)
                    TakeAnswer++;
                else if (Answer == 0)
                    await DisplayAlert("Оповещение", "Нужно выбрать ответ!", "Хорошо");
                else
                    FalseAnswer++;
            }
            else if (VoprosList.Count == i && AnswerIt)
            {
                if (TrueAnswer == Answer)
                    TakeAnswer++;
                else if (Answer == 0)
                    await DisplayAlert("Оповещение", "Нужно выбрать ответ!", "Хорошо");
                else
                    FalseAnswer++;
                AnswerIt = false;
            }

            if (VoprosList.Count > i)
            {

                LabelNameVopros.Text = VoprosList[0 + i];
                var1.Text = VoprosList[1 + i];
                var2.Text = VoprosList[2 + i];
                var3.Text = VoprosList[3 + i];
                var4.Text = VoprosList[4 + i];
                TrueAnswer = Convert.ToInt32(VoprosList[5 + i]);
                if (!String.IsNullOrEmpty(VoprosList[6 + i]))
                    webImage.Source = ImageSource.FromUri(new Uri(VoprosList[6 + i]));
                else
                    webImage.Source = ImageSource.FromUri(new Uri("http://imtis.ru/no.jpg"));
                i += 7;
            }
            else
            {
                if (TaskList.Count > y && TheTaskIs)
                {
                    if (!String.IsNullOrEmpty(AnswerTask.Text))
                    {
                        TextAnswer[z] = AnswerTask.Text.ToString();
                        z++;
                        y += 2;
                    }
                    else
                        await DisplayAlert("Оповещение", "Нужно заполнить форму ответа на задачу!", "Хорошо");

                    if (TaskList.Count > y && TheTaskIs)
                    {
                        btnPutTestAnswer.Text = "Решить задачу!";
                        Variant.IsVisible = false;
                        AnswerTask.IsVisible = true;
                        LabelNameVopros.Text = TaskList[0 + y];
                        if (!String.IsNullOrEmpty(TaskList[1 + y]))
                            webImage.Source = ImageSource.FromUri(new Uri(TaskList[1 + y]));
                        else
                            webImage.Source = ImageSource.FromUri(new Uri("http://imtis.ru/no.jpg"));
                    }
                    else
                    {
                        btnPutTestAnswer.Text = "Закончить тестирование!";
                        DependencyService.Get<IMySQL>().PutAnswerTest(FalseAnswer, TakeAnswer, TextAnswer);
                        await Navigation.PopAsync();
                    }
                }
                else
                {
                    btnPutTestAnswer.Text = "Закончить тестирование!";
                    DependencyService.Get<IMySQL>().PutAnswerTest(FalseAnswer, TakeAnswer, TextAnswer);
                    await Navigation.PopAsync();
                }
            }
        }
    }
}