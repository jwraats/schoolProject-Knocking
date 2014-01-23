using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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

        public GamePage()
        {
            this.InitializeComponent();

            switch(kd.Difficult)
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            KnockingDoors kd = e.Parameter as KnockingDoors;
            if (kd != null)
            {
                this.kd = kd;
            }
        }

    }
}
