using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace knocking_doors.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        KnockingDoors kd;
        SolidColorBrush brush = new SolidColorBrush();
        double myDbl = 0;

        public GamePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            KnockingDoors kd = e.Parameter as KnockingDoors;
            brush.Color = Color.FromArgb(255, 0, 0, 255);
            if (kd != null)
            {
                this.kd = kd;
                switch (kd.Difficult)
                {
                    case KnockingDoors.Difficulties.First_Time:
                        DiffText.Text = "First Timer";
                        break;
                    case KnockingDoors.Difficulties.Been_Here:
                        DiffText.Text = "Been Here";
                        break;
                    case KnockingDoors.Difficulties.Born_Here:
                        DiffText.Text = "Born Here";
                        break;
                    default:
                        DiffText.Text = "First Timer";
                        break;
                }
            }
        }

        public void UpdateDistanceIcon(double d)
        {
            if (d > 1 | d < 0)
                return;

            if (d < 0.2)
                DistanceText.Text = "Je bent koud";
            else if (d < 0.4)
                DistanceText.Text = "Je bent lauw";
            else if (d < 0.6)
                DistanceText.Text = "Je begint in de buurt te komen";
            else if (d < 0.8)
                DistanceText.Text = "Je bent warm!";
            else if (d < 0.95)
                DistanceText.Text = "Je bent heel erg warm!";
            else
                DistanceText.Text = "Heet! Heet! Heet!";

            byte red = (byte)Math.Floor(255*d);
            byte blue = (byte)Math.Floor(255-255*d);
            DistanceIcon.Fill = new SolidColorBrush(Color.FromArgb(255, red, 0, blue));
        }

        private void changeDoor()
        {
            kd.changeDoor();
        }
    }
}
