using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using SADM.Helpers;
using SADM.Models;
using SADM.Services;
using SADM.Views;
using Xamarin.Forms;

namespace SADM.ViewModels
{
    public class BalanceViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private readonly IHudService _hudService;

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
        protected string value1;
        protected string value2;
        protected string value3;
        protected string value4;
        protected string value5;
        protected string value6;
        protected string value7;
        protected string value8;
        protected string value9;
        protected string value10;
        protected string value11;
        protected string value12;

        protected string month1;
        protected string month2;
        protected string month3;
        protected string month4;
        protected string month5;
        protected string month6;
        protected string month7;
        protected string month8;
        protected string month9;
        protected string month10;
        protected string month11;
        protected string month12;
        protected int maxBilled;
        protected int maxBar;

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
        public string Value1 { get => value1; set => SetProperty(ref value1, value); }
        public string Value2 { get => value2; set => SetProperty(ref value2, value); }
        public string Value3 { get => value3; set => SetProperty(ref value3, value); }
        public string Value4 { get => value4; set => SetProperty(ref value4, value); }
        public string Value5 { get => value5; set => SetProperty(ref value5, value); }
        public string Value6 { get => value6; set => SetProperty(ref value6, value); }
        public string Value7 { get => value7; set => SetProperty(ref value7, value); }
        public string Value8 { get => value8; set => SetProperty(ref value8, value); }
        public string Value9 { get => value9; set => SetProperty(ref value9, value); }
        public string Value10 { get => value10; set => SetProperty(ref value10, value); }
        public string Value11 { get => value11; set => SetProperty(ref value11, value); }
        public string Value12 { get => value12; set => SetProperty(ref value12, value); }
        public string Month1 { get => month1; set => SetProperty(ref month1, value); }
        public string Month2 { get => month2; set => SetProperty(ref month2, value); }
        public string Month3 { get => month3; set => SetProperty(ref month3, value); }
        public string Month4 { get => month4; set => SetProperty(ref month4, value); }
        public string Month5 { get => month5; set => SetProperty(ref month5, value); }
        public string Month6 { get => month6; set => SetProperty(ref month6, value); }
        public string Month7 { get => month7; set => SetProperty(ref month7, value); }
        public string Month8 { get => month8; set => SetProperty(ref month8, value); }
        public string Month9 { get => month9; set => SetProperty(ref month9, value); }
        public string Month10 { get => month10; set => SetProperty(ref month10, value); }
        public string Month11 { get => month11; set => SetProperty(ref month11, value); }
        public string Month12 { get => month12; set => SetProperty(ref month12, value); }

        public int MaxBilled { get => maxBilled; set => SetProperty(ref maxBilled, value); }
        public int MaxBar { get => maxBar; set => SetProperty(ref maxBar, value); }

        public Command PayAttemptCommand { get; private set; }

        public BalanceViewModel(INavigationService navigationService, 
                                 ISettingsService settingsService, 
                                 IHudService hudService, 
                                 ISadmApiService apiService) : 
        base(navigationService, settingsService, hudService, apiService)
        {
            _navigationService = navigationService;
            _hudService = hudService;
            PayAttemptCommand = new Command(PayAttemptCommandExecuted);//(async () => await HudService.ShowErrorAsync("Servicio no disponible por el momento."));
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
                PeriodOfConsumption = balance.ExpirationDate.AddMonths(-1).ToString("dd/MM/yy") + " - " + balance.ExpirationDate.ToString("dd/MM/yy");//"DD/MM/AA - DD/MM/AA";
                processGraph();
            }
        }

        private async void PayAttemptCommandExecuted()
        {
            /*if ((float)balance.TotalDebt <= 0) {
                await _hudService.ShowInformationAsync("Tu saldo es de 0");
                return; 
            } */
            var parameter = new NavigationParameters()
            {
                {"Balance",balance}
            };

            await _navigationService.NavigateAsync(nameof(PayPage), parameter);
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
            MaxBilled = (int)maxValue;
            MaxBar= (int) GetHeight(Convert.ToDouble(maxBilled), maxValue);
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

            Value1 = values[0].ToString();
            Value2 = values[1].ToString();
            Value3 = values[2].ToString();
            Value4 = values[3].ToString();
            Value5 = values[4].ToString();
            Value6 = values[5].ToString();
            Value7 = values[6].ToString();
            Value8 = values[7].ToString();
            Value9 = values[8].ToString();
            Value10 = values[9].ToString();
            Value11 = values[10].ToString();
            Value12 = values[11].ToString();

            Month1  = getMonth(1);
            Month2  = getMonth(2);
            Month3  = getMonth(3);
            Month4  = getMonth(4);
            Month5  = getMonth(5);
            Month6  = getMonth(6);
            Month7  = getMonth(7);
            Month8  = getMonth(8);
            Month9  = getMonth(9);
            Month10  =  getMonth(10);
            Month11  =  getMonth(11);
            Month12  =  getMonth(12);

                           } 
        protected string getMonth(int month){

            var lastMonth = billedMonth.Month;
            var selectMonth= month+lastMonth+1;
            if(selectMonth>12) selectMonth-=12;
            switch(selectMonth)
            {
                case 1: return "EN";
                case 2: return "FB";
                case 3: return "MR";
                case 4: return "AB";
                case 5: return "MY";
                case 6: return "JN";
                case 7: return "JL";
                case 8: return "AG";
                case 9: return "SP";
                case 10: return "OC";
                case 11: return "NV";
                case 12: return "DC";

            }

            return "";
        }

        protected Double GetHeight(double value, double maxValue)
        {
            return value * MAX_HEIGHT_BAR / maxValue;
        }
    }
}
