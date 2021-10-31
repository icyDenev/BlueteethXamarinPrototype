using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using BTeethPrototype;
using BTeethPrototype.Activities;
using BTeethPrototype.Adapters;
using Firebase.Auth;
using IdentityModel.OidcClient.Browser;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using static Android.Support.Design.Widget.BottomNavigationView;
using static Android.Views.View;
using Button = Android.Widget.Button;
using RelativeLayout = Android.Widget.RelativeLayout;

namespace BTeethPrototype
{
    [Activity(Label = "DashBoard", Theme = "@style/AppTheme")]
    public class DashBoard : AppCompatActivity, IOnCompleteListener
    {
        Button startButton;
        BottomNavigationView bnv;
        TextView helloTextView, coverageTextView, movementsTextView;
        RelativeLayout activity_dashboard;
        int timerStart = -1;
        Stopwatch stopwatch;
        System.Threading.Timer _timer;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.dashboard);
            
            activity_dashboard = FindViewById<RelativeLayout>(Resource.Id.activity_dashboard);
            
            helloTextView = FindViewById<TextView>(Resource.Id.db_layout_hello_text);

            coverageTextView = FindViewById<TextView>(Resource.Id.db_coverage);
            movementsTextView = FindViewById<TextView>(Resource.Id.db_movements);

            helloTextView.Text = "Hello, " + MainActivity.loginResult.User.Identity.Name;

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Dashboard";

            ImageView iv = FindViewById<ImageView>(Resource.Id.db_layout_ul_img);
            iv.SetImageResource(Resource.Drawable.UL);
            
            iv = FindViewById<ImageView>(Resource.Id.db_layout_um_img);
            iv.SetImageResource(Resource.Drawable.UM);
            
            iv = FindViewById<ImageView>(Resource.Id.db_layout_ur_img);
            iv.SetImageResource(Resource.Drawable.UR);
            
            iv = FindViewById<ImageView>(Resource.Id.db_layout_dl_img);
            iv.SetImageResource(Resource.Drawable.DL);
            
            iv = FindViewById<ImageView>(Resource.Id.db_layout_dm_img);
            iv.SetImageResource(Resource.Drawable.DM);
            
            iv = FindViewById<ImageView>(Resource.Id.db_layout_dr_img);
            iv.SetImageResource(Resource.Drawable.DR);
            
            startButton = FindViewById<Button>(Resource.Id.db_layout_start_btn);
            startButton.Click += OnStartBtnClick;

            bnv = FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation);
            bnv.NavigationItemSelected += OnNavigationItemSelected;

            stopwatch = new Stopwatch();
        }


        private void OnStartBtnClick(object sender, EventArgs e)
        {
            timerStart *= (-1);

            if (timerStart == 1)
            {
                stopwatch.Start();

                coverageTextView.Visibility = ViewStates.Visible;
                movementsTextView.Visibility = ViewStates.Visible;
                helloTextView.Visibility = ViewStates.Invisible;

                _timer = new System.Threading.Timer((o) =>
                {
                    RunOnUiThread(() =>
                    {
                        var currentTime = stopwatch.Elapsed.Multiply(3);

                        startButton.Text = "Brushing Time\n" + (currentTime.Minutes / 10) + (currentTime.Minutes % 10) + ":" + (currentTime.Seconds / 10) + (currentTime.Seconds % 10);

                        var drawable = coverageTextView.Background.Current as GradientDrawable;
                        drawable.SetColor(Color.ParseColor(getColorFromProgress(currentTime.TotalSeconds / 100)));
                        coverageTextView.Text = "Coverage:\n" + (int)currentTime.TotalSeconds + "%";

                        double randTempl = Convert.ToDouble((new Random().NextDouble() * 2).ToString("N6"));
                        drawable = movementsTextView.Background.Current as GradientDrawable;
                        drawable.SetColor(Color.ParseColor(getColorFromProgress((currentTime.TotalSeconds + randTempl) / 100)));
                        movementsTextView.Text = "Movements:\n" + (int)(currentTime.TotalSeconds + randTempl) + "/100";
                    });
                }
                , null, 0, 1000);
            }
            else
            {
                stopwatch.Reset();

                startButton.Text = "Start Brushing";

                coverageTextView.Visibility = ViewStates.Invisible;
                movementsTextView.Visibility = ViewStates.Invisible;
                helloTextView.Visibility = ViewStates.Visible;

                _timer.Dispose();
            }
        }

        string getColorFromProgress(double pr)
        {
            double r, g, b = 0;
            r = 1.0; g = 0;
            if (pr <= 0.5) g = pr / 0.5;
            else { g = 1; r = (1 - pr) / 0.5; }
            r *= 150; r += 50;
            g *= 150; g += 50;
            string str = "#" + Convert.ToInt32(Math.Round(r)).ToString("X2") + Convert.ToInt32(Math.Round(g)).ToString("X2") + Convert.ToInt32(Math.Round(b)).ToString("X2");
            return str;
        }

        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful == true)
            {
                Snackbar snackbar = Snackbar.Make(activity_dashboard, "Password has been Changed!", Snackbar.LengthShort);
                snackbar.Show();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.toolbar_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.menu_home)
            {
                /*StartActivity(new Intent(this, typeof(DashBoard)));
                Finish();*/
            }
            else if (item.ItemId == Resource.Id.navigation_dashboard)
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

        public void OnNavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            if (e.Item.ItemId == Resource.Id.navigation_dashboard)
            {
                StartActivity(new Intent(this, typeof(BrushingHistoryActivity)));
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