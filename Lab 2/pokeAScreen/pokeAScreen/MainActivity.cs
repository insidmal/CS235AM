using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace pokeAScreen
{
    [Activity(Label = "pokeAScreen", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

               SetContentView (Resource.Layout.Main);
            //instantiate poke button and create click event
            var firstPoke = FindViewById<Button>(Resource.Id.firstPokeButton);
            firstPoke.Click += (sender, e) => {
                //instantiate intent, add extra, start activity
                var second = new Intent(this, typeof(SecondActivity));
                second.PutExtra("poke", true);
                StartActivity(second);

            };
            //instantiate hi button and add click event
            var firstHi = FindViewById<Button>(Resource.Id.firstHiButton);
            firstHi.Click += (sender, e) => {
                //instantiate intent, add extra, start activity
                var second = new Intent(this, typeof(SecondActivity));
                second.PutExtra("text", "Hi from screen 1");
                StartActivity(second);
            };
        }
    }
}

