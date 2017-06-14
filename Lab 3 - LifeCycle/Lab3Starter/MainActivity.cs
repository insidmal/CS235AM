using Android.App;
using Android.Widget;
using Android.OS;
using System.Xml.Serialization;
using System.IO;
using Android.Util;


namespace Lab3
{
    [Activity(Label = "Lab3", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        QuoteBank quoteCollection;
        TextView quotationTextView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            if (savedInstanceState != null)
            {

                // Deserialized the saved object state

                XmlSerializer x = new XmlSerializer(typeof(QuoteBank));
                string xml = savedInstanceState.GetString("Quotes");


                    quoteCollection = (QuoteBank)x.Deserialize(new StringReader(xml));


            }
            else {
                // Create the quote collection and display the current quote
                quoteCollection = new QuoteBank();
                quoteCollection.LoadQuotes();
                quoteCollection.GetNextQuote();
            }
            quotationTextView = FindViewById<TextView>(Resource.Id.quoteTextView);
            quotationTextView.Text = quoteCollection.CurrentQuote.Quotation;

            TextView authorTextView = FindViewById<TextView>(Resource.Id.quoteAuthorTextView);
            authorTextView.Text = quoteCollection.CurrentQuote.Person;



            // Display another quote when the "Next Quote" button is tapped
            var nextButton = FindViewById<Button>(Resource.Id.nextButton);
            nextButton.Click += delegate {
                quoteCollection.GetNextQuote();
                quotationTextView.Text = quoteCollection.CurrentQuote.Quotation;
                authorTextView.Text = quoteCollection.CurrentQuote.Person;
            };

            var saveButton = FindViewById<Button>(Resource.Id.saveButton);
            saveButton.Click += delegate
            {
                Quote q = new Quote();
                q.Quotation = FindViewById<EditText>(Resource.Id.newQuoteText).Text;

                q.Person = FindViewById<EditText>(Resource.Id.newQuoteAuthor).Text;

                
                quoteCollection.Quotes.Add(q);

                FindViewById<EditText>(Resource.Id.newQuoteAuthor).Text = "";
                FindViewById<EditText>(Resource.Id.newQuoteText).Text = "";

            };
        }

        protected override void OnSaveInstanceState(Bundle outState)

        {
            StringWriter s = new StringWriter();
            XmlSerializer x = new XmlSerializer(typeof(QuoteBank));
            x.Serialize(s, quoteCollection);
            string xs = s.ToString();
            outState.PutString("Quotes", xs);




            base.OnSaveInstanceState(outState);
        }
    }

}

