using knocking_doors.Controller;
using knocking_doors.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace knocking_doors
{
    public class KnockingDoors
    {
        private MapController mc;
        private Frame rootFrame;
        public Player Player { get; set; }

        public KnockingDoors()
        {
            mc = new MapController();
            Player = new Player();
        }

        public KnockingDoors(Frame rootFrame) : base()
        {
            this.rootFrame = rootFrame;
            this.changePage(typeof(MainPage));
        }

        public void changePage(Type page)
        {
            this.rootFrame.Navigate(page, this);
        }
    }
}
