﻿using System;
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
using MapApp.CustomControls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace MapApp.UWP.CustomControls
{
    public sealed partial class CustomPinControl : UserControl
    {
        public CustomPinControl(CustomPin pin)
        {
            this.InitializeComponent();
            Name.Text = pin.Name;
            Description.Text = pin.Description;
            Rating.Text = pin.Rating.ToString();
        }
    }
}
