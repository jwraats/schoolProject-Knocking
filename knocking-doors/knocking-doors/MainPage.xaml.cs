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

namespace knocking_doors
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private KnockingDoors kd;

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
           base.OnNavigatedTo(e);
           KnockingDoors kd = e.Parameter as KnockingDoors;
           if (kd != null)
           {
               this.kd = kd;
           }
        }

        private void Proceed_Click(object sender, RoutedEventArgs e)
        {
            kd.Player.Name = tbName.Text;
            kd.changePage(typeof(View.LevelPage));
        }
    }
}
