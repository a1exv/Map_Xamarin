using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace MapApp.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            Xamarin.FormsMaps.Init("vxQvPhixS4ofXMeTczgs~cc07KmHZI7YcJBVxQsxjUA~Ahbcoqk_ev0To-ufNsys_1tV9S7dKAKT1lrJhkGvqX3j6j9Z0i13w7OFgXsU-AQY");
            LoadApplication(new MapApp.App());
        }
    }
}
