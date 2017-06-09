using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace diplom_mob1
{
    public class NavPagecs : NavigationPage
    {
        public NavPagecs()
        {
            new NavigationPage(new Student())
            {
                Title = "Student",
            };
            new NavigationPage(new ResultTest())
            {
                Title = "ResultTest",
            };
            new NavigationPage(new ListTest())
            {
                Title = "ListTest",
            };
            new NavigationPage(new QRScanner())
            {
                Title = "QRScanner",
            };
        }
    }
}