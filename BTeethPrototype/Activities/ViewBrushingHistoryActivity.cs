using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Gms.Tasks;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using BTeethPrototype.Adapters;
using BTeethPrototype.DataHelper;
using BTeethPrototype.Models;
using Firebase.Firestore;
using static Android.Views.View;

namespace BTeethPrototype
{
    [Activity(Label = "ViewBrushingHistory", Theme = "@style/AppTheme")]
    public class ViewBrushingHistoryActivity : AppCompatActivity, IOnClickListener
    {

        private string hist_id;

        private string[] months = new string[]{"", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

        private string GetDate(DateTime d)
        {
            string s = "";
            s += d.Day + " ";
            s += months[d.Month] + " " + d.Year;
            return s;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.view_histories);

            hist_id = Intent.GetStringExtra("id");   // TODO: get brushing history from db by its ID
            BrushingHistory bh = new BrushingHistory(); // search from data base with wherefieldcontains + setup db connection here

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "View history details";

            ImageView iv = FindViewById<ImageView>(Resource.Id.bh_layout_ul_img);
            iv.SetImageResource(Resource.Drawable.UL);
            iv.SetColorFilter(new Color(200, 200, 20, Convert.ToInt32(Math.Round(150.0 * (bh.ZoneUL) / 100))));
            iv = FindViewById<ImageView>(Resource.Id.bh_layout_um_img);
            iv.SetImageResource(Resource.Drawable.UM);
            iv.SetColorFilter(new Color(200, 200, 20, Convert.ToInt32(Math.Round(150.0 * (bh.ZoneM) / 100))));
            iv = FindViewById<ImageView>(Resource.Id.bh_layout_ur_img);
            iv.SetImageResource(Resource.Drawable.UR);
            iv.SetColorFilter(new Color(200, 200, 20, Convert.ToInt32(Math.Round(150.0 * (bh.ZoneUR) / 100))));
            iv = FindViewById<ImageView>(Resource.Id.bh_layout_dl_img);
            iv.SetImageResource(Resource.Drawable.DL);
            iv.SetColorFilter(new Color(200, 200, 20, Convert.ToInt32(Math.Round(150.0 * (bh.ZoneDL) / 100))));
            iv = FindViewById<ImageView>(Resource.Id.bh_layout_dm_img);
            iv.SetImageResource(Resource.Drawable.DM);
            iv.SetColorFilter(new Color(200, 200, 20, Convert.ToInt32(Math.Round(150.0 * (bh.ZoneM) / 100))));
            iv = FindViewById<ImageView>(Resource.Id.bh_layout_dr_img);
            iv.SetImageResource(Resource.Drawable.DR);
            iv.SetColorFilter(new Color(200, 200, 20, Convert.ToInt32(Math.Round(150.0 * (bh.ZoneDR) / 100))));

            FindViewById<TextView>(Resource.Id.bh_layout_date_txt).Text = "Date:" + GetDate(bh.StartTime);

            FindViewById<TextView>(Resource.Id.bh_layout_time_txt).Text = "Brushing Time:\n" + "2:33";// (bh.EndTime-bh.StartTime).Minutes + ":" + ((bh.EndTime - bh.StartTime).Seconds<10? "0":"") + (bh.EndTime - bh.StartTime).Seconds;
        }

        public void OnClick(View v)
        {
            /*if(v.Id == Resource.Id.back_button)
            {
                StartActivity(new Intent(this, typeof(BrushingHistoryActivity)));
                Finish();
            }*/
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.toolbar_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            string str;
            if (item.ItemId == Resource.Id.menu_home)
            {
                StartActivity(new Intent(this, typeof(DashBoard)));
                Finish();
            }
            else if (item.ItemId == Resource.Id.menu_bh)
            {
                StartActivity(new Intent(this, typeof(BrushingHistoryActivity)));
                Finish();
            }
            else if (item.ItemId == Resource.Id.menu_settings)
            {
                /*StartActivity(new Intent(this, typeof(BrushingHistoryActivity)));
                Finish();*/
            }

            return base.OnOptionsItemSelected(item);
        }

    }
}