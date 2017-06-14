using Android.App;
using Android.Widget;
using Android.OS;

namespace HelloAndroid
{
    [Activity(Label = "HelloAndroid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            //create layout
            base.OnCreate(bundle);
            var layout = new LinearLayout(this);

            layout.Orientation = Orientation.Vertical;


            //create ui elements
            var aLabel = new TextView(this);
            aLabel.SetText(Resource.String.helloLabelText);

            var aButton = new Button(this);

            //add click event
            aButton.SetText(Resource.String.helloButtonText);
            aButton.Click += (sender, e) =>
            {
                aLabel.Text = "Hello from the button";
            };

            //instantiate reset button and add click event
            var resetButton = new Button(this);
            resetButton.SetText(Resource.String.resetButtonText);
            resetButton.Click += (sender, e) => {
                aLabel.SetText(Resource.String.helloLabelText);
            };

            //add ui elements to layout
            layout.AddView(aButton);
            layout.AddView(aLabel);
            layout.AddView(resetButton);
            SetContentView(layout);
        }
    }
}

