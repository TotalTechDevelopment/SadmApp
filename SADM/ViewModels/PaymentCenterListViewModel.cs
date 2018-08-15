using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Prism.Navigation;
using SADM.Controls;
using SADM.Models;
using SADM.Services;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace SADM.ViewModels
{
    public class PaymentCenterListViewModel : ViewModelBase
    {
        protected bool showMap;
        protected string search;
        protected Pin pinSelected;
        protected TimeSpan? duration;
        protected CameraUpdate cameraUpdate;

        public CameraUpdate CameraUpdate { get => cameraUpdate; set => SetProperty(ref cameraUpdate, value); }
        public TimeSpan? Duration { get => duration; set => SetProperty(ref duration, value); }
        public Pin PinSelected 
        { 
            get => pinSelected; 
            set
            {
                SetProperty(ref pinSelected, value);
                if(value != null)
                {
                    CameraUpdate = CameraUpdateFactory.NewPositionZoom(value.Position, 10d);
                }
            }
        }
        public bool ShowMap { get => showMap; set => SetProperty(ref showMap, value); }
        public string Search 
        { 
            get => search; 
            set 
            {
                SetProperty(ref search, value);
                UpdateList();
            } 
        }

        public ICommand ShowPaymentCenterCommand { get; private set; }
        public ObservableCollectionExt<PaymentCenter> PaymentCenterList { get; private set; }
        public ObservableCollection<Pin> PinList { get; set; }

        public PaymentCenterListViewModel(INavigationService navigationService, 
                                          ISettingsService settingsService, 
                                          IHudService hudService, 
                                          ISadmApiService apiService) : 
        base(navigationService, settingsService, hudService, apiService)
        {
            PaymentCenterList = new ObservableCollectionExt<PaymentCenter>();
            PinList = new ObservableCollection<Pin>();
            ShowPaymentCenterCommand = new Command(OnPaymentCenterSelected);
            ShowMap = true;
            Duration = TimeSpan.FromSeconds(1);
        }

        protected void UpdateList()
        {
            var list = Settings.AppConfiguration.Values.PaymentCenterList;
            PaymentCenterList.Reset(list);
            PinList.Clear();
            var latitudeList = new List<double>();
            var longitudeList = new List<double>();
            foreach(var paymentCenter in list)
            {
                PinList.Add(new Pin
                {
                    Label = paymentCenter.Name,
                    Address = paymentCenter.Address,
                    Position = new Position(paymentCenter.Latitude, paymentCenter.Longitude)
                });
                latitudeList.Add(paymentCenter.Latitude);
                longitudeList.Add(paymentCenter.Longitude);
            }
            //show all pins in map
            if(PinList.Any())
            {
                CameraUpdate = CameraUpdateFactory.NewBounds(
                    new Bounds(new Position(latitudeList.Min(), longitudeList.Min()), 
                               new Position(latitudeList.Max(), longitudeList.Max())),
                    50);
                if(PinList.Count == 1)
                {
                    PinSelected = PinList.First();
                }
            }
        }

        protected void OnPaymentCenterSelected(object obj)
        {
            if(obj is PaymentCenter paymentCenter)
            {
                PinSelected = PinList[PaymentCenterList.IndexOf(paymentCenter)];
                ShowMap = true;
            } 
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            UpdateList();
        }
    }
}