﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            StackLayout stackLayout = new StackLayout();

            //qr start

            zxing = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            zxing.OnScanResult += (result) =>
                Device.BeginInvokeOnMainThread(async () => {

                    // Stop analysis until we navigate away so we don't keep reading barcodes
                    zxing.IsAnalyzing = false;

                    // Take SQL
                    Student.idTest = Convert.ToInt32(result.Text);
                    //display
                    //await DisplayAlert("Scanned Barcode", result.Text + " == " + Student.idTest, "OK");
                    // Navigate away
                    await Navigation.PopModalAsync();
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

            //qr end

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
        private async void btnBack_Click(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }


        //qr start
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