using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using DateTime = System.DateTime;

namespace TechiesFlights
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void HideExplore()
        {            
            ExplorePane.HeightRequest = 0;
            ExplorePane.IsVisible = false;
            ExplorePane.Opacity = 0;
        }

        private void ShowExplore()
        {   
            ExplorePane.HeightRequest = 0;
            ExplorePane.IsVisible = true;
            ExplorePane.HeightRequest = 170;
            ExplorePane.FadeTo(1);
        }
    }
}
