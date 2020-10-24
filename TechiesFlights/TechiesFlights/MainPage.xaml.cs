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
        private double page_height;

        public MainPage()
        {
            InitializeComponent();
        }
        private object _lockPane = new object();
        private DateTime lastPaneTime;
        private const double trothlingMs = 5;
        private double paneY = 0;
        private double minimunInfoPageHeight = 300;

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            page_height = Height;
        }

        private void PanGestureRecognizer_OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            Debug.WriteLine(e.TotalY);

            var now = DateTime.UtcNow;
            if ((now - lastPaneTime).Milliseconds < trothlingMs)
            {
                lastPaneTime = DateTime.UtcNow;
                return;
            }
            
            lastPaneTime = DateTime.UtcNow;

            ResizePane(paneY,e.TotalY,e.StatusType);
        }

        private const double upperPoint = 500;
        private const double lowerPoint = 300;
        private const double smoothFactor = 0.15;
        private void ResizePane(double oldY, double y, GestureStatus eStatusType)
        {
            y = oldY + smoothFactor*(y - oldY);
            
            Debug.WriteLine($"Avg: {y}");
            
            var height = InfoPane.HeightRequest + -1 * y;
            height = Math.Max(Math.Min(height, page_height),minimunInfoPageHeight);
            InfoPane.HeightRequest = height;

            if (eStatusType == GestureStatus.Completed || eStatusType == GestureStatus.Canceled)
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
