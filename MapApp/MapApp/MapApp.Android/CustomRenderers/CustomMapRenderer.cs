using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;
using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MapApp.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Maps.Android;
using MapApp.Droid.CustomRenderers;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace MapApp.Droid.CustomRenderers
{
    public class CustomMapRenderer:MapRenderer, GoogleMap.IInfoWindowAdapter
    {
        private List<CustomPin> CustomPins { get; set; }
        private GoogleMap _map;
        private bool _isDrawn;
        public CustomMapRenderer(Context context) : base(context)
        {
            
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null)
            {
               

            }
            if (e.NewElement != null)
            {
                var formsMap = (CustomMap) e.NewElement;
                CustomPins = formsMap.CustomPins;
                Control.GetMapAsync(this);
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l,t,r,b);
            if (changed)
            {
                _isDrawn = false;
            }
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName.Equals("CustomPins"))
            {
                _isDrawn = false;
            }
            if (e.PropertyName.Equals("VisibleRegion")&&!_isDrawn||e.PropertyName.Equals("CustomPins"))
            {
                CustomPins = ((CustomMap)Element).CustomPins;
                _map.Clear();
                if (CustomPins != null)
                {
                    foreach (var pin in CustomPins)
                    {
                        var marker = CreateMarker(pin);
                        _map.AddMarker(marker);
                    }
                    _isDrawn = true;
                }
            }
        }

        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);
            _map = map;
            _map.MapClick += GoogleMap_MapClick;
            NativeMap.SetInfoWindowAdapter(this);
        }

        protected override MarkerOptions CreateMarker(Pin pin)
        {
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
            marker.SetTitle(pin.Label);
            marker.SetSnippet(pin.Address);
            
            return marker;
        }

        public Android.Views.View GetInfoWindow(Marker marker)
        {
            return null;
        }

        public Android.Views.View GetInfoContents(Marker marker)
        {
            var inflater =
                Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as
                    Android.Views.LayoutInflater;
            if (inflater != null)
            {
                Android.Views.View view;
                var customPin = GetCustomPin(marker);
                if (customPin == null)
                {
                    throw new Exception("Custom pin not found");
                }
                view = inflater.Inflate(Resource.Layout.MapInfoWindow, null);
                var InfoName = view.FindViewById<TextView>(Resource.Id.InfoWindowName);
                var InfoDescription = view.FindViewById<TextView>(Resource.Id.InfoWindowDescription);
                var InfoRating = view.FindViewById<TextView>(Resource.Id.InfoWindowRating);
                var pin = GetCustomPin(marker);
                InfoName.Text = pin.Name;
                InfoDescription.Text = pin.Description;
                InfoRating.Text = pin.Rating.ToString();
                return view;
            }
            return null;
        }

        CustomPin GetCustomPin(Marker marker)
        {
            var position = new Position(marker.Position.Latitude, marker.Position.Longitude);
            foreach (var pin in CustomPins)
            {
                if (pin.Position == position)
                {
                    return pin;
                }
            }
            return null;
        }

        void GoogleMap_MapClick(object sender, GoogleMap.MapClickEventArgs e)
        {
            ((CustomMap)Element).SetLocation(e.Point.Latitude, e.Point.Longitude);
        }
    }
}