﻿using knocking_doors.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
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
        private KnockingDoors kd;
        private SolidColorBrush brush = new SolidColorBrush();
        private string totalDistance;
        Player player;
        private bool inGame = true;
        private double hotCold, totalDistanceDbl, distanceToGoDbl;
        private DispatcherTimer dTimer = new DispatcherTimer();

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
                        DiffText.Text = "Nieuw hier";
                        break;
                    case KnockingDoors.Difficulties.Been_Here:
                        DiffText.Text = "Bekend terrein";
                        break;
                    case KnockingDoors.Difficulties.Born_Here:
                        DiffText.Text = "Hometown";
                        break;
                    default:
                        DiffText.Text = "Nieuw hier";
                        break;
                }
                this.changeDoor();  //Eerste keer deur maken!

                //Databinding naar player
                player = kd.Player;
                ScoreText.DataContext = player;
                Binding binding = new Binding() { Path = new PropertyPath("Score") };
                ScoreText.SetBinding(TextBlock.TextProperty, binding);

                //Verander de timer
                //Timer aTimer = new Timer(new TimerCallback(timeAction), null, 0, 1000);
                dTimer.Interval = TimeSpan.FromSeconds(1);
                dTimer.Tick += timeAction;
                dTimer.Start();
            }
        }
        private void timeAction(object state, object e)
        {
                if (kd.Player.currentDoor != null)
                {
                    if (kd.Player.currentDoor.TimeLeft <= 1 && inGame)    //Jammer!! Maar niet optijd!
                    {

                        inGame = false;
                        kd.Player.ScoreBind -= 10;
                        changeDoor();
                    }
                    else
                    {
                        kd.Player.currentDoor.TimeLeft -= 1;    //Aftellen!
                    }

                }
                kd.changePlayerLocation();




                string distanceToGo = kd.mc.GetDistanceBetweenPoints(kd.Player.Latitude, kd.Player.Longitude, kd.Player.currentDoor.Latitude, kd.Player.currentDoor.Longitude);
                if (distanceToGo != "Onbekend")
                {
                    distanceToGoDbl = Convert.ToDouble(distanceToGo);
                    //Oplosing omdat Geofence zo buggie is :)
                    if (distanceToGoDbl <= 35 && inGame) //het is dus binnen 35 meter!
                    {
                        inGame = false;
                        kd.Player.ScoreBind += (5 * kd.Player.currentDoor.TimeLeft) + 10;   //Super mooie berekening
                        //Gewonnen!
                        changeDoor();
                    }

                    if (totalDistance != "Onbekend")
                    {
                        totalDistanceDbl = Convert.ToDouble(totalDistance);
                        //Zorgen dat het warm/koud element het doet
                        hotCold = (distanceToGoDbl - totalDistanceDbl) / totalDistanceDbl;

                        //Zorgen dat deze threath elkaar niet tegenspreekt!
                        UpdateDistanceIcon(hotCold);
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


            DistanceText.Text += "\n";
            if(kd.Player.currentDoor.TimeLeft > 0){
                if (kd.Player.currentDoor.TimeLeft >= 60)
                {
                    DistanceText.Text += (kd.Player.currentDoor.TimeLeft / 60)+" m ";
                }
                DistanceText.Text += (kd.Player.currentDoor.TimeLeft % 60) + " s";
            }
            else
            {
                DistanceText.Text += "Geen tijd meer over";
            }

            byte red = (byte)Math.Floor(255*d);
            byte blue = (byte)Math.Floor(255-255*d);
            DistanceIcon.Fill = new SolidColorBrush(Color.FromArgb(255, red, 0, blue));
        }


        private void changeDoor()
        {
            kd.changeDoor();
            inGame = true;
            Address.Text = kd.Player.currentDoor.Address;
            Address.Text += kd.Player.currentDoor.Latitude + "," + kd.Player.currentDoor.Longitude;
            totalDistance = kd.mc.GetDistanceBetweenPoints(kd.Player.Latitude, kd.Player.Longitude, kd.Player.currentDoor.Latitude, kd.Player.currentDoor.Longitude);
            DistanceFromDoorText.Text = totalDistance+" Meters";
            DoorPanel.ImageSource = kd.ImageStreet;
        }

        private void NewDoorBtn_Click(object sender, RoutedEventArgs e)
        {
            kd.Player.ScoreBind -= 10;
            changeDoor();
        }

        private void BackToDiffBtn_Click(object sender, RoutedEventArgs e)
        {
            dTimer.Stop();
            kd.changePlayerLocation();
            kd.changePlayerImage();
            kd.changePage(typeof(View.LevelPage));
        }
    }
}
