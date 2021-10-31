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
using BTeethPrototype.Models;

namespace BTeethPrototype.Adapters
{
    interface IRedirectable
    {
        void Redirect(string id = "");
        void Redirect(BrushingHistory bh = null);

    }
}