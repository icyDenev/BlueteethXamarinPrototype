using Android.App;
using Android.Widget;
using Android.OS;
using Firebase;
using Firebase.Auth;
using static Android.Views.View;
using Android.Views;
using Android.Support.Design.Widget;
using Android.Gms.Tasks;
using Android.Graphics;
using Android.Gms.Common.Apis;
using Android.Gms.Common;
using Android.Support.V7.App;
using BTeethPrototype.DataHelper;
using Firebase.Firestore;
using Auth0.OidcClient;
using Android.Content;
using Java.IO;
using IdentityModel.OidcClient;

namespace BTeethPrototype
{
    [Activity(Label = "BlueteethPrototype", Theme = "@style/AppTheme", LaunchMode = Android.Content.PM.LaunchMode.SingleTask)]
    [IntentFilter(
    new[] { Intent.ActionView },
    Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
    DataScheme = "com.companyname.bteethprototype",
    DataHost = "blueteeth.eu.auth0.com",
    DataPathPrefix = "/android/com.companyname.bteethprototype/callback")]
    public class MainActivity : AppCompatActivity, IOnClickListener
    {
        public static Auth0Client client;
        public static LoginResult loginResult;
        Button btnLogin;
        RelativeLayout activity_main;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            client = new Auth0Client(new Auth0ClientOptions
            {
                Domain = "blueteeth.eu.auth0.com",
                ClientId = "rvGare8llynokzFOXeqeIcXlWV3V5tjN",
                Scope = "openid profile email"
            });

            //TODO: Implement current user check
            // Set our view from the "main" layout resource  
            SetContentView(Resource.Layout.activity_main);

            //Views  
            btnLogin = FindViewById<Button>(Resource.Id.login_btn_login);
            activity_main = FindViewById<RelativeLayout>(Resource.Id.activity_main);
            btnLogin.SetOnClickListener(this);
        }

        public void OnClick(View v)
        {
            if (v.Id == Resource.Id.login_btn_login)
            {
                LoginUserAsync();
            }
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);

            Auth0.OidcClient.ActivityMediator.Instance.Send(intent.DataString);
        }

        private async System.Threading.Tasks.Task LoginUserAsync()
        {
            loginResult = await client.LoginAsync();

            if (loginResult.IsError)
            {
                Snackbar snackbar = Snackbar.Make(activity_main, $"{client.GetUserInfoAsync(loginResult.AccessToken)}, Register Failed! Check your Internet Connection", Snackbar.LengthShort);
                snackbar.Show();
                return; 
            }

            StartActivity(new Android.Content.Intent(this, typeof(DashBoard)));
            Finish();
        }
    }
}