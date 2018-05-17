using System;
using System.Collections.Generic;
using System.Text;
using MapApp.CustomControls;
using MapKit;

namespace MapApp.iOS.CustomControls
{
    public class CustomMKAnnotationView:MKAnnotationView
    {
        public string Name { get; set; }
        public string PinDescription { get; set; }
        public int Rating { get; set; }
        public CustomMKAnnotationView(IMKAnnotation annotation, string id):base(annotation, id)
        {
         
        }
    }
}
