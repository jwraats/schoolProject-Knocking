﻿using System;
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
    public sealed partial class LevelPage : Page
    {
        private KnockingDoors kd;
        

        public LevelPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            KnockingDoors kd = e.Parameter as KnockingDoors;
            if (kd != null)
            {
                this.kd = kd;
                if(kd.Player != null && kd.Player.Name != ""){
                    lbl.Text = "Hallo " + kd.Player.Name + ", hoe bekend ben je hier?";
                    imgFirstButton.ImageSource = kd.ImageStreet;
                }
            }
        }

        private void startGame()
        {
            kd.Player.Level.Longitude = kd.Player.Longitude;
            kd.Player.Level.Latitude = kd.Player.Latitude;
            kd.changePage(typeof(View.GamePage));
        }

        private void First_Time_Click(object sender, RoutedEventArgs e)
        {
            kd.Difficult = KnockingDoors.Difficulties.First_Time;
            startGame();
        }

        private void Been_Here_Click(object sender, RoutedEventArgs e)
        {
            kd.Difficult = KnockingDoors.Difficulties.Been_Here;
            startGame();
        }

        private void Born_Here_Click(object sender, RoutedEventArgs e)
        {
            kd.Difficult = KnockingDoors.Difficulties.Born_Here;
            startGame();
        }
    }
}
