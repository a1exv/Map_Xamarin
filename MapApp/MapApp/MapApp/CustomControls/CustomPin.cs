using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace MapApp.CustomControls
{
    public class CustomPin:Pin
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
    }
}
