using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PigClasses
{
    public class Die
    {
        int sides;
        Random rnd = new Random();
        public Die(int numSides)
        {
            sides = numSides;

        }

        public int Roll()
        {
            return rnd.Next(1, sides+1);
        }

    }
}