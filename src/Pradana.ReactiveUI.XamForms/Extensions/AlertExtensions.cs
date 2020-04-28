using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Pradana.ReactiveUI.XamForms.Extensions
{
    public static class AlertExtensions
    {
        public static Task DisplayError(this Page page, string errorMessage)
        {
            return page.DisplayAlert("Error", errorMessage, "OK");
        }

        public static Task DisplayError(this Page page, Exception error)
        {
            return page.DisplayAlert("Error", error.Message, "OK");
        }
    }
}
