using System;
using System.Collections.Generic;
using System.Text;
using CoreGraphics;
using MapApp.CustomControls;
using MapApp.iOS.CustomControls;
using MapApp.iOS.CustomRenderers;
using MapKit;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;


[assembly:ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace MapApp.iOS.CustomRenderers
{
    public class CustomMapRenderer:MapRenderer
    {
        UIView customPinView;
        List<CustomPin> customPins;
        private UITapGestureRecognizer tapRecognizer;

        

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null)
            {
                var nativeMap = Control as MKMapView;
                if (nativeMap != null)
                {
                    nativeMap.RemoveAnnotations(nativeMap.Annotations);
                    nativeMap.GetViewForAnnotation = null;
                    nativeMap.RemoveGestureRecognizer(tapRecognizer);
                    tapRecognizer = null;
                    nativeMap.DidSelectAnnotationView -= OnDidSelectAnnotationView;
                    nativeMap.DidDeselectAnnotationView -= OnDidDeselectAnnotationView;
                }
                
            }
            if (e.NewElement != null)
            {
                tapRecognizer=new UITapGestureRecognizer(OnTap) {NumberOfTapsRequired = 1, NumberOfTouchesRequired = 1};

                var formsMap = (CustomMap) e.NewElement;
                var nativeMap = Control as MKMapView;
                customPins = formsMap.CustomPins;
                nativeMap.AddGestureRecognizer(tapRecognizer);
                nativeMap.GetViewForAnnotation = GetViewForAnnotation;
                nativeMap.DidSelectAnnotationView += OnDidSelectAnnotationView;
                nativeMap.DidDeselectAnnotationView += OnDidDeselectAnnotationView;
            }
        }

        MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            MKAnnotationView annotationView = null;
            if (annotation is MKUserLocation)
            {
                return null;
            }
            var customPin = GetCustomPin(annotation as MKPointAnnotation);
            if (customPin == null)
            {
                throw new Exception("Custom pin not found");
            }
            annotationView = mapView.DequeueReusableAnnotation(customPin.Id.ToString());
            if (annotationView == null)
            {
                annotationView = new CustomMKAnnotationView(annotation, customPin.Id.ToString());
                annotationView.CalloutOffset=new CGPoint(0,0);
                ((CustomMKAnnotationView) annotationView).Name = customPin.Name;
                ((CustomMKAnnotationView) annotationView).PinDescription = customPin.Description;
                ((CustomMKAnnotationView) annotationView).Rating = customPin.Rating;
            }
            return annotationView;
            
        }

        CustomPin GetCustomPin(MKPointAnnotation annotation)
        {
            var position = new Position(annotation.Coordinate.Latitude, annotation.Coordinate.Longitude);
            foreach (var pin in customPins)
            {
                if (pin.Position == position)
                {
                    return pin;
                }
            }
            return null;
        }

        void OnDidSelectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            var customView = e.View as CustomMKAnnotationView;
            customPinView = new UIView();
            customPinView.Frame = new CGRect(0,0,200,100);
            var label = new UILabel();
            label.Frame = new CGRect(0,0,200,100);
            label.Text = String.Format(customView.Name+"\n"+customView.PinDescription+"\nRating: "+customView.Rating);
            customPinView.AddSubview(label);
            customPinView.Center=new CGPoint(0, -(e.View.Frame.Height+10));
            e.View.AddSubview(customPinView);
        }

        void OnDidDeselectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            if (!e.View.Selected)
            {
                customPinView.RemoveFromSuperview();
                customPinView.Dispose();
                customPinView = null;
            }
        }

        private void OnTap(UITapGestureRecognizer recognizer)
        {
            var cgPoint = recognizer.LocationInView(Control);
            var location = ((MKMapView) Control).ConvertPoint(cgPoint, Control);
            ((CustomMap)Element).SetLocation(location.Latitude, location.Longitude);
        }
       
    }
}
