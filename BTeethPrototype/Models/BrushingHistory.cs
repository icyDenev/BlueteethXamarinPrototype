using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BTeethPrototype.Models
{
    public class BrushingHistory
    {
        public BrushingHistory()
        {
            Random rng = new Random();
            Id = "0";
            StartTime = DateTime.Now;
            EndTime = DateTime.Now;
            ZoneDL = rng.Next(0, 100);
            ZoneDR = rng.Next(0, 100);
            ZoneM = rng.Next(0, 100);
            ZoneUL = rng.Next(0, 100);
            ZoneUR = rng.Next(0, 100);
            Progress = rng.Next(0, 100);
            AuthId = "asd";
        }

        public int getMovements()
        {
            return ZoneDL + ZoneDR + ZoneM + ZoneUL + ZoneUR;
        }

        public string Id { get; set; }
        
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int Progress { get; set; }

        public int ZoneDL { get; set; }

        public int ZoneDR { get; set; }

        public int ZoneM { get; set; }

        public int ZoneUL { get; set; }

        public int ZoneUR { get; set; }

        public string AuthId { get; set; }
    }
}