using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text.Format;
using Android.Views;
using Android.Widget;
using BTeethPrototype.Models;

namespace BTeethPrototype.DataHelper
{
    //This is a Converter for Firestore's Timestamp and CS's DateTime
    public class DateTimeTimestampConverter
    {
        public static DateTime TimestampToDateTimeConvert (string timestamp)
        {
            int year = Convert.ToInt32(timestamp.Substring(30, 4));
            int month = Months.getMonthNumberBy3Letters(timestamp.Substring(4, 3));
            int day = Convert.ToInt32(timestamp.Substring(8, 2));
            int hour = Convert.ToInt32(timestamp.Substring(11, 2));
            int minute = Convert.ToInt32(timestamp.Substring(14, 2));
            int second = Convert.ToInt32(timestamp.Substring(17, 2));

            return new DateTime(year, month, day, hour, minute, second);
        }
        public string DateTimeToTimestampConvert(DateTime dateTime)
        {
            return dateTime.Year + "-"
                + (dateTime.Month / 10) * 10 + dateTime.Month % 10 + "-"
                + (dateTime.Day / 10) * 10 + dateTime.Day % 10 + "T"
                + (dateTime.Hour / 10) * 10 + dateTime.Hour % 10 + ":"
                + (dateTime.Minute / 10) * 10 + dateTime.Minute % 10 + ":"
                + (dateTime.Second / 10) * 10 + dateTime.Second % 10 + "Z";
        }
    }
}