
using Android.App;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;

namespace BTeethPrototype.DataHelper
{
    public class AppDataHelper
    {
        public static FirebaseFirestore GetDatabase()
        {
            var app = GetApp();
            FirebaseFirestore database;

            database = FirebaseFirestore.GetInstance(app);

            return database;
        }
        public static FirebaseApp GetApp()
        {
            var app = FirebaseApp.InitializeApp(Application.Context);

            if (app == null)
            {
                var options = new FirebaseOptions.Builder()
                    .SetApplicationId("blueteethprototype")
                    .SetApiKey("AIzaSyA8HVWiiNkJszs73MWKT5dKBLL6LxDkhzY")
                    .SetDatabaseUrl("https://blueteethprototype.firebaseio.com")
                    .SetStorageBucket("blueteethprototype.appspot.com")
                    .SetProjectId("blueteethprototype")
                    .Build();

                app = FirebaseApp.InitializeApp(Application.Context, options);
            }

            return app;
        }
    }
}