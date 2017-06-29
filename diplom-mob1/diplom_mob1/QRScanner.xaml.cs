using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace diplom_mob1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QRScanner : ContentPage
    {
        ZXingScannerView zxing;
        ZXingDefaultOverlay overlay;
        Button btnBack;
        public QRScanner() : base()
        {
            Title = "Сканирование QR-кода";
            StackLayout stackLayout = new StackLayout();
            List<String> ListNameId = new List<String>();

            zxing = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            zxing.OnScanResult += (result) =>
                Device.BeginInvokeOnMainThread(async () => {
                    zxing.IsAnalyzing = false;
                    Student.idTest = Convert.ToInt32(result.Text);
                    DependencyService.Get<IMySQL>().PutTakeStudentTest(Student.idTest, Student.idStudent);
                    await Navigation.PopAsync();
                });

            overlay = new ZXingDefaultOverlay
            {
                ShowFlashButton = zxing.HasTorch,
            };
            overlay.FlashButtonClicked += (sender, e) => {
                zxing.IsTorchOn = !zxing.IsTorchOn;
            };
            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            grid.Children.Add(zxing);
            grid.Children.Add(overlay);

            btnBack = new Button
            {
                Text = "Назад",
                VerticalOptions = LayoutOptions.End
            };

            btnBack.Clicked += btnBack_Click;

            stackLayout.Children.Add(grid);
            stackLayout.Children.Add(btnBack);
            this.Content = stackLayout;
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
             Navigation.PopModalAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            zxing.IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            zxing.IsScanning = false;
            base.OnDisappearing();
        }
    }
}
