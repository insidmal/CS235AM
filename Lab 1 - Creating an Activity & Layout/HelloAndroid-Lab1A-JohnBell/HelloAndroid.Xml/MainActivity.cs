using Android.App;
using Android.Widget;
using Android.OS;

namespace HelloAndroid.Xml
{
    [Activity(Label = "HelloAndroid.Xml", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            //instantiate ui elements
            var aButton = FindViewById<Button>(Resource.Id.aButton);
            var aLabel = FindViewById<TextView>(Resource.Id.aLabel);
            var resetButton = FindViewById<Button>(Resource.Id.resetButton);

            //create click events
            aButton.Click += (sender, e) =>
            {
                aLabel.Text = "Hello from the button";
            };

            resetButton.Click += (sender, e) =>
            {
                aLabel.SetText(Resource.String.helloLabelText);
            };

        }
    }
}

