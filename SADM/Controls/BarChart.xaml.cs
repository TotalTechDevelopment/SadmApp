using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Specialized;
using System;

namespace SADM.Controls
{
    public partial class BarChart : ImprovedFrame
    {
        protected bool OnCollectionChangedIsSet;

        /// <summary>
        /// Identifies the ItemsSource attached property.
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(IList<double>), typeof(BarChart));

        /// <summary>
        /// Accessors
        /// </summary>
        public IList<double> ItemsSource
        {
            get { return (IList<double>)GetValue(ItemsSourceProperty); }
            set => SetValue(ItemsSourceProperty, value);
        }

        public BarChart()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if(propertyName == ItemsSourceProperty.PropertyName)
            {
                if(ItemsSource is ObservableCollection<double> oc && !OnCollectionChangedIsSet)
                {
                    oc.CollectionChanged += UpdateView;
                    UpdateView(null, null);
                }
            }
        }

        protected void UpdateView(object sender, NotifyCollectionChangedEventArgs e)
        {
            var barWidth = Math.Round(WidthRequest / 26);
            WidthRequest = barWidth * 26;
            ChartStckLy.Padding = new Thickness(barWidth / 2, barWidth, barWidth * 1.5, 0);
            var maxValue = ItemsSource.Max();
            var height = HeightRequest - barWidth;
            var barColor = (Color)Application.Current.Resources["BarChartBarColor"];
            ChartStckLy.Children.Clear();
            for (var i = 0; i < 12 && i < ItemsSource.Count; i++)
            {
                ChartStckLy.Children.Add(GetBar(ItemsSource[i], barWidth, height, maxValue, barColor));
            }
            for (var i = ItemsSource.Count; i < 12;i++)
            {
                ChartStckLy.Children.Add(GetBar(0 , barWidth, height, maxValue, BackgroundColor));
            }
        }

        protected Label GetBar(double value, double width, double height, double maxValue, Color color)
        {
            return new Label
            {
                HeightRequest = Math.Round(value * height / maxValue),
                WidthRequest = width,
                BackgroundColor = color,
                Margin = new Thickness(width, 0, 0, 0),
                VerticalOptions = LayoutOptions.End
            };
        }
    }
}
