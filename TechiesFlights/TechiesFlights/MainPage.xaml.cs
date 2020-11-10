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

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            page_height = Height;
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

        private double oldY = 300;
        private const double smoothFactor = 0.25;

        private const double upperPoint = 450;
        private const double lowerPoint = 300;

        private double page_height = 0;

        private void PanGestureRecognizer_OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            Debug.WriteLine($"Total Y: {e.TotalY}");

            var y = e.TotalY;

            y = smoothFactor * (y - oldY);

            var height = InfoPane.HeightRequest + -1 * y;

            height = Math.Max(Math.Min(height, page_height), lowerPoint);

            InfoPane.HeightRequest = height;

            oldY = y;

            if (e.StatusType == GestureStatus.Completed || e.StatusType == GestureStatus.Canceled)
            {
                var toUpper = Math.Abs(upperPoint - height);
                var toBottom = Math.Abs(lowerPoint - height);

                if (toUpper < toBottom)
                {
                    InfoPane.HeightRequest = upperPoint;
                    ShowExplore();
                }
                else
                {
                    InfoPane.HeightRequest = lowerPoint;
                    HideExplore();
                }
            }
        }
    }
}
