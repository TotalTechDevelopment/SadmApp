using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using SADM.Helpers;
using SADM.Models;
using SADM.Services;

namespace SADM.ViewModels
{
    public class BalanceViewModel : ViewModelBase
    {
        protected const double MAX_HEIGHT_BAR = 70;
        protected Balance balance;

        protected float? totalToPay;
        protected float? outstandingDebt;
        protected DateTime expiration;
        protected DateTime billedMonth;
        protected string periodOfConsumption;
        protected Double bar1;
        protected Double bar2;
        protected Double bar3;
        protected Double bar4;
        protected Double bar5;
        protected Double bar6;
        protected Double bar7;
        protected Double bar8;
        protected Double bar9;
        protected Double bar10;
        protected Double bar11;
        protected Double bar12;
        public float? TotalToPay { get => totalToPay; set => SetProperty(ref totalToPay, value); }
        public float? OutstandingDebt { get => outstandingDebt; set => SetProperty(ref outstandingDebt, value); }
        public DateTime Expiration { get => expiration; set => SetProperty(ref expiration, value); }
        public DateTime BilledMonth { get => billedMonth; set => SetProperty(ref billedMonth, value); }
        public string PeriodOfConsumption { get => periodOfConsumption; set => SetProperty(ref periodOfConsumption, value); }
        public Double Bar1 { get => bar1; set => SetProperty(ref bar1, value); }
        public Double Bar2 { get => bar2; set => SetProperty(ref bar2, value); }
        public Double Bar3 { get => bar3; set => SetProperty(ref bar3, value); }
        public Double Bar4 { get => bar4; set => SetProperty(ref bar4, value); }
        public Double Bar5 { get => bar5; set => SetProperty(ref bar5, value); }
        public Double Bar6 { get => bar6; set => SetProperty(ref bar6, value); }
        public Double Bar7 { get => bar7; set => SetProperty(ref bar7, value); }
        public Double Bar8 { get => bar8; set => SetProperty(ref bar8, value); }
        public Double Bar9 { get => bar9; set => SetProperty(ref bar9, value); }
        public Double Bar10 { get => bar10; set => SetProperty(ref bar10, value); }
        public Double Bar11 { get => bar11; set => SetProperty(ref bar11, value); }
        public Double Bar12 { get => bar12; set => SetProperty(ref bar12, value); }

        public IAsyncCommand PayAttemptCommand { get; private set; }

        public BalanceViewModel(INavigationService navigationService, 
                                 ISettingsService settingsService, 
                                 IHudService hudService, 
                                 ISadmApiService apiService) : 
        base(navigationService, settingsService, hudService, apiService)
        {
            PayAttemptCommand = new AsyncCommand(async () => await HudService.ShowErrorAsync("Servicio no disponible por el momento."));
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            if (parameters.ContainsKey(string.Empty))
            {
                balance = parameters.GetValue<Balance>(string.Empty);

                TotalToPay = balance.TotalDebt;
                OutstandingDebt = balance.TotalDebt;
                Expiration = balance.ExpirationDate;
                BilledMonth = balance.BilledMonth;
                PeriodOfConsumption = "DD/MM/AA - DD/MM/AA";
                processGraph();
            }
        }

        protected void processGraph()
        {
            var parts = Enumerable.Range(0, balance.Graph.Length / 19).Select(i => balance.Graph.Substring(i * 19, 19));
            var list = new List<double>();
            foreach(var v in parts)
            {
                list.Add(double.Parse(v.Substring(0, 9)));
            }
            DrawBarChart(list.ToArray());
        }

        protected void DrawBarChart(params double[] values)
        {
            var maxValue = values.Max();
            Bar1 = GetHeight(values[0], maxValue);
            Bar2 = GetHeight(values[1], maxValue);
            Bar3 = GetHeight(values[2], maxValue);
            Bar4 = GetHeight(values[3], maxValue);
            Bar5 = GetHeight(values[4], maxValue);
            Bar6 = GetHeight(values[5], maxValue);
            Bar7 = GetHeight(values[6], maxValue);
            Bar8 = GetHeight(values[7], maxValue);
            Bar9 = GetHeight(values[8], maxValue);
            Bar10 = GetHeight(values[9], maxValue);
            Bar11 = GetHeight(values[10], maxValue);
            Bar12 = GetHeight(values[11], maxValue);
        }

        protected Double GetHeight(double value, double maxValue)
        {
            return value * MAX_HEIGHT_BAR / maxValue;
        }
    }
}
