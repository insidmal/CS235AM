using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace HelloMultiScreen
{
    [Activity(Label = "HelloMultiScreen", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //load ui
            SetContentView(Resource.Layout.Main);

            //add click event to button
            var showSecond = FindViewById<Button>(Resource.Id.showSecond);
            showSecond.Click += (sender, e) => {
                //create intent to pass data
               var second = new Intent(this, typeof(SecondActivity));
                second.PutExtra("FirstData", "Data from FirstACtivity");
                StartActivity(second);
            };
        }
    }
}

