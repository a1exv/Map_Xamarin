using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Maps;
using MapApp.CustomControls;
using MapApp.UWP.CustomControls;
using MapApp.UWP.CustomRenderers;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.UWP;
using Xamarin.Forms.Platform.UWP;

[assembly:ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace MapApp.UWP.CustomRenderers
{
    public class CustomMapRenderer:MapRenderer
    {
        MapControl nativaMap;
        List<CustomPin> customPins;
        CustomPinControl customPinControl;
        bool CustomPinControlShown = false;


        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null)
            {
                nativaMap.MapElementClick -= OnMapElementClick;
                nativaMap.MapTapped -= NativaMap_MapTapped;
                nativaMap.Children.Clear();
                customPinControl = null;
                nativaMap = null;
            }
            if (e.NewElement != null)
            {
                var formsMap = (CustomMap) e.NewElement;
                nativaMap=Control as MapControl;
                customPins = formsMap.CustomPins;

                nativaMap.Children.Clear();
                nativaMap.MapElementClick += OnMapElementClick;
                
                nativaMap.MapTapped += NativaMap_MapTapped;
                if (customPins != null)
                {
                    foreach (var pin in customPins)
                    {
                        var snPosition = new BasicGeoposition()
                        {
                            Latitude = pin.Position.Latitude,
                            Longitude = pin.Position.Longitude
                        };
                        var snPoint = new Geopoint(snPosition);
                        var mapIcon = new MapIcon();
                        mapIcon.Location = snPoint;
                        nativaMap.MapElements.Add(mapIcon);
                    }
                }
            }
        }

        private void NativaMap_MapTapped(MapControl sender, MapInputEventArgs args)
        {
            ((CustomMap) Element).SetLocation(args.Location.Position.Latitude, args.Location.Position.Longitude);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("CustomPins"))
            {
                customPins = ((CustomMap)Element).CustomPins;
                if (customPins != null)
                {
                    foreach (var pin in customPins)
                    {
                        var snPosition = new BasicGeoposition()
                        {
                            Latitude = pin.Position.Latitude,
                            Longitude = pin.Position.Longitude
                        };
                        var snPoint = new Geopoint(snPosition);
                        var mapIcon = new MapIcon();
                        mapIcon.Location = snPoint;
                        nativaMap.MapElements.Add(mapIcon);
                    }
                }
            }
        }

        private void OnMapElementClick(MapControl sender, MapElementClickEventArgs e)
        {
            var mapIcon = e.MapElements.FirstOrDefault(x => x is MapIcon) as MapIcon;

            if (mapIcon != null)
            {
                if (!CustomPinControlShown)
                {
                    var customPin = GetCustomPin(mapIcon.Location.Position);
                    if (customPin == null)
                    {
                        throw new Exception("Custom pin not found");
                    }
                    customPinControl = new CustomPinControl(customPin);

                    var customPinControls = nativaMap.Children.Where(x => x is CustomPinControl);
                    foreach (var control in customPinControls)
                    {
                        nativaMap.Children.Remove(control);
                    }
                    var snPosition = new BasicGeoposition()
                    {
                        Latitude = customPin.Position.Latitude,
                        Longitude = customPin.Position.Longitude
                    };
                    var snPoint = new Geopoint(snPosition);
                    nativaMap.Children.Add(customPinControl);

                    MapControl.SetLocation(customPinControl, snPoint);
                    MapControl.SetNormalizedAnchorPoint(customPinControl, new Windows.Foundation.Point(0.5, 1.0));
                    CustomPinControlShown = true;
                }
                else
                {
                    nativaMap.Children.Remove(customPinControl);
                    CustomPinControlShown = false;
                }
            }
        }

        private CustomPin GetCustomPin(BasicGeoposition geopos)
        {
            var pos = new Position(geopos.Latitude, geopos.Longitude);
            foreach (var pin in customPins)
            {
                if (pin.Position == pos)
                {
                    return pin;
                }
            }
            return null;
        }

    

    }
}
