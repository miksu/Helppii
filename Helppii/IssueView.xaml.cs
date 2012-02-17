﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls.Maps;
using System.Device.Location;

namespace helppii
{
    public partial class IssueView : PhoneApplicationPage
    {
        GeoCoordinateWatcher watcher;
        ItemViewModel item;

        public IssueView()
        {
            InitializeComponent();

            watcher = new GeoCoordinateWatcher();
            watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);
            watcher.MovementThreshold = 25;
            watcher.Start();
        }

        void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            textBlock2.Text = "Distance: " + Math.Round(item.Location.GetDistanceTo(e.Position.Location), 0) + "m";

            //Pushpin pin1 = new Pushpin();
            //pin1.Location = e.Position.Location;
            //myMap.Children.Add(pin1);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string selectedIndex = "";
            if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
            {
                int index = int.Parse(selectedIndex);
                item = App.ViewModel.Items[index];
                DataContext = item;
                textBlock2.Text = item.Location.ToString();
            }
        }
    }
}