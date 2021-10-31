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
    [Activity(Label = "BrushingHistories", Theme = "@style/AppTheme")]
    public class BrushingHistoryActivity : AppCompatActivity, IOnSuccessListener, IRedirectable
    {
        List<BrushingHistory> BHList = new List<BrushingHistory>();
        RecyclerView BHRecyclerView;
        RelativeLayout brushing_histories;
        FirebaseFirestore database;
        BottomNavigationView bnv;
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.brushing_histories);

            //FindViewById<Button>(Resource.Id.test_button).SetOnClickListener(this);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Brushing History";

            bnv = FindViewById<BottomNavigationView>(Resource.Id.bh_bottom_navigation);
            bnv.NavigationItemSelected += OnNavigationItemSelected;

            brushing_histories = FindViewById<RelativeLayout>(Resource.Id.brushing_histories);
            BHRecyclerView = FindViewById<RecyclerView>(Resource.Id.brushingHistoryRecyclerView);
            database = AppDataHelper.GetDatabase();

            RetrieveData();
            SetupRecyclerView();
        }

        public void Redirect(string id)
        {
            var ac = new Intent(this, typeof(ViewBrushingHistoryActivity));
            ac.PutExtra("id", id);
            StartActivity(ac);
            Finish();
        }

        public void Redirect(BrushingHistory bh = null)
        {
            var ac = new Intent(this, typeof(ViewBrushingHistoryActivity));
            ac.PutExtra("TimeStartToEnd", (bh.EndTime - bh.StartTime).TotalSeconds.ToString());
            StartActivity(ac);
            Finish();
        }

        public void RetrieveData()
        {
            database.Collection("BrushingHistory").Get().AddOnSuccessListener(this);
        }

        private void SetupRecyclerView()
        {
            BHRecyclerView.SetLayoutManager(new LinearLayoutManager(BHRecyclerView.Context));
            DataAdapter adapter = new DataAdapter(BHList, this);
            BHRecyclerView.SetAdapter(adapter);
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            var snapshot = (QuerySnapshot)result;
            
            if (!snapshot.IsEmpty)
            {
                var documents = snapshot.Documents;
                BHList.Clear();
                foreach (DocumentSnapshot BHData in documents)
                {
                    BrushingHistory brushingHistory = new BrushingHistory();
                    try
                    {
                        brushingHistory.Id = BHData.Id;
                        brushingHistory.StartTime = DateTimeTimestampConverter.TimestampToDateTimeConvert(BHData.Get("StartTime").ToString());
                        brushingHistory.EndTime = DateTimeTimestampConverter.TimestampToDateTimeConvert(BHData.Get("EndTime").ToString());
                        brushingHistory.AuthId = BHData.Get("AccountUID").ToString();
                        brushingHistory.ZoneDL = Convert.ToInt32(BHData.Get("ZoneDL"));
                        brushingHistory.ZoneDR = Convert.ToInt32(BHData.Get("ZoneDR"));
                        brushingHistory.ZoneUL = Convert.ToInt32(BHData.Get("ZoneUL"));
                        brushingHistory.ZoneUR = Convert.ToInt32(BHData.Get("ZoneUR"));
                        brushingHistory.ZoneM = Convert.ToInt32(BHData.Get("ZoneM"));

                        BHList.Add(brushingHistory);
                    }
                    catch
                    { }
                }
            }

            SetupRecyclerView();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.toolbar_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            string str;
            if (item.ItemId == Resource.Id.navigation_home)
            {
                StartActivity(new Intent(this, typeof(DashBoard)));
                Finish();
            }
            else if(item.ItemId == Resource.Id.menu_bh)
            {
                /*StartActivity(new Intent(this, typeof(BrushingHistoryActivity)));
                Finish();*/
            }
            else if(item.ItemId == Resource.Id.menu_settings)
            {
                /*StartActivity(new Intent(this, typeof(BrushingHistoryActivity)));
                Finish();*/
            }

            return base.OnOptionsItemSelected(item);
        }

        public void OnNavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            if (e.Item.ItemId == Resource.Id.navigation_home)
            {
                StartActivity(new Intent(this, typeof(DashBoard)));
                Finish();
            }
            else if (e.Item.ItemId == Resource.Id.navigation_notifications)
            {
                StartActivity(new Intent(this, typeof(SettingsActivity)));
                Finish();
            }
        }
    }
}