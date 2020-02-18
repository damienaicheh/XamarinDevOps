using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinDevOps.ViewModels;

namespace XamarinDevOps.Forms
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var config = await new MainViewModel().LoadConfigurationAsync();
            MyLabel.Text = config.BaseUrl;
        }
    }
}
