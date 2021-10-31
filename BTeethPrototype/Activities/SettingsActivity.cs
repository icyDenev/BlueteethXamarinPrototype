using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Support.Design.Widget;
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
    [Activity(Label = "Settings", Theme = "@style/AppTheme")]
    public class SettingsActivity : AppCompatActivity
    {
        BottomNavigationView bnv;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.settings);

            //FindViewById<Button>(Resource.Id.test_button).SetOnClickListener(this);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Settings";

            bnv = FindViewById<BottomNavigationView>(Resource.Id.settings_bottom_navigation);
            bnv.NavigationItemSelected += OnNavigationItemSelected;
        }

        public void OnNavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            if (e.Item.ItemId == Resource.Id.navigation_home)
            {
                StartActivity(new Intent(this, typeof(DashBoard)));
                Finish();
            }
            else if (e.Item.ItemId == Resource.Id.navigation_dashboard)
            {
                StartActivity(new Intent(this, typeof(BrushingHistoryActivity)));
                Finish();
            }
        }

    }
}