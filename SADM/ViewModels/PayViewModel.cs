using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Navigation;
using SADM.Extensions;
using SADM.Helpers;
using SADM.Models;
using SADM.Models.Requests;
using SADM.Services;

namespace SADM.ViewModels
{
    public class PayViewModel : ViewModelBase
    {
        private Balance balance;
        protected ISadmApiService sadmApiService;
 

        #region Properties
        private string urlWeb;
        public string UrlWeb
        {
            get { return urlWeb; }
            set
            {
                SetProperty(ref urlWeb, value);
            }
        }
        #endregion

        public PayViewModel(ISadmApiService sadmApiService, INavigationService navigationService, ISettingsService settingsService, IHudService hudService, ISadmApiService apiService) : base(navigationService, settingsService, hudService, apiService)
        {
            this.sadmApiService = sadmApiService;
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Balance"))
            {
                balance = parameters.GetValue<Balance>("Balance");
            }
            VPCRequest conn = new VPCRequest();
            conn.AddDigitalOrderField("vpc_Version", SADM.Settings.AppConfiguration.Values.vpc_Version);
            conn.AddDigitalOrderField("vpc_Command", SADM.Settings.AppConfiguration.Values.vpc_Command);
            conn.AddDigitalOrderField("vpc_AccessCode", SADM.Settings.AppConfiguration.Values.vpc_AccessCode);
            conn.AddDigitalOrderField("vpc_Merchant", SADM.Settings.AppConfiguration.Values.vpc_Merchant);
            conn.AddDigitalOrderField("vpc_ReturnURL", "http://localhost:8080/api/");
            conn.AddDigitalOrderField("vpc_MerchTxnRef", "PruebaRfId2529");
            conn.AddDigitalOrderField("vpc_OrderInfo", "2529");
            conn.AddDigitalOrderField("vpc_Amount", (balance.TotalDebt * 100).ToString());
            conn.AddDigitalOrderField("vpc_Currency", SADM.Settings.AppConfiguration.Values.vpc_Currency);
            //conn.AddDigitalOrderField("vpc_CustomPaymentPlanPlanId", vpc_CustomPaymentPlanPlanId.Text);
            conn.AddDigitalOrderField("vpc_Locale", SADM.Settings.AppConfiguration.Values.vpc_Locale);
            // Perform the transaction
            string url = conn.Create3PartyQueryString();
            url = SADM.Settings.AppConfiguration.Values.PaymentBaseUrl + url;
            UrlWeb = url;
        }
        //Balance balance;
        //protected string cardHolder;
        //protected string cardNumber;
        //protected string month;
        //protected string year;
        //protected string cvv;

        //public string CardHolder { get => cardHolder; set => SetProperty(ref cardHolder, value); }
        //public string CardNumber { get => cardNumber; set => SetProperty(ref cardNumber, value); }
        //public string Month { get => month; set => SetProperty(ref month, value); }
        //public string Year { get => year; set => SetProperty(ref year, value); }
        //public string Cvv { get => cvv; set => SetProperty(ref cvv, value); }

        //public IEnumerable<string> MonthList { get; private set; }
        //public IEnumerable<string> YearList { get; private set; }

        //public IAsyncCommand ShowMonthListCommand { get; private set; }
        //public IAsyncCommand ShowYearListCommand { get; private set; }
        //public IAsyncCommand ShowHelpCvvCommand { get; private set; }
        //public IAsyncCommand PayAttemptCommand { get; private set; }

        //public PayViewModel(INavigationService navigationService,
        //                    ISettingsService settingsService,
        //                    IHudService hudService,
        //                    ISadmApiService apiService) :
        //base(navigationService, settingsService, hudService, apiService)
        //{
        //    Month = AppResources.MonthPlaceholder;
        //    Year = AppResources.YearPlaceholder;
        //    MonthList = GenerateMonthList();
        //    YearList = GenerateYearList();

        //    ShowMonthListCommand = new AsyncCommand(ShowMonthListAsync);
        //    ShowYearListCommand = new AsyncCommand(ShowYearListAsync);
        //    ShowHelpCvvCommand = new AsyncCommand(ShowHelpCvvAsync);
        //    PayAttemptCommand = new AsyncCommand(PayAttemptAsync);
        //}

        //protected async Task ShowMonthListAsync()
        //{
        //    Month = await HudService.ShowListAsync(AppResources.MonthPlaceholder, MonthList, "Cancelar") ?? AppResources.MonthPlaceholder;
        //}

        //protected async Task ShowYearListAsync()
        //{
        //    Year = await HudService.ShowListAsync(AppResources.YearPlaceholder, YearList, "Cancelar") ?? AppResources.YearPlaceholder;
        //}

        //protected async Task ShowHelpCvvAsync()
        //{
        //    await HudService.ShowExampleAsync(AppResources.CvvHelp, "cvv_example.png");
        //}

        //protected async Task PayAttemptAsync()
        //{
        //    if(await UserInputsAreValids())
        //    {
        //        var data = new DataCardModel
        //        {
        //            CardHolder = CardHolder,
        //            Cvv = Cvv,
        //            Month = Month,
        //            CardNumber = CardNumber,
        //            Year = Year,
        //            Amount = balance.TotalDebt
        //        };
        //        await GoToPageAsync<Views.PaymentBanamex>(data);
        //        //await HudService.ShowInformationAsync("PENDIENTE POR DEFINIR");
        //    }
        //}

        //protected async Task<bool> UserInputsAreValids()
        //{
        //    var errorList = new List<string>();
        //    errorList.AddRange(CardHolder.GetCardHolderErrorList());
        //    errorList.AddRange(CardNumber.GetCardNumberErrorList());
        //    errorList.AddRange(Cvv.GetCvvErrorList());
        //    if(Year == AppResources.YearPlaceholder)
        //    {
        //        errorList.Add(AppResources.YearEmpty);
        //    }
        //    if (Month == AppResources.MonthPlaceholder)
        //    {
        //        errorList.Add(AppResources.MonthEmpty);
        //    }
        //    if (errorList.Any())
        //    {
        //        await HudService.ShowErrorListAsync(errorList);
        //    }
        //    return !errorList.Any();
        //}

        //protected List<string> GenerateMonthList()
        //{
        //    var list = new List<string>();
        //    for (var i = 1; i <= 12; i++)
        //    {
        //        list.Add(i < 10 ? $"0{i}" : i.ToString());
        //    }
        //    return list;
        //}

        //protected List<string> GenerateYearList()
        //{
        //    var list = new List<string>();
        //    for (var i = DateTime.Now.Year; i <= DateTime.Now.Year + 5; i++)
        //    {
        //        list.Add((i).ToString().Substring(2, 2));
        //    }
        //    return list;
        //}

        //public override void OnNavigatedTo(NavigationParameters parameters)
        //{
        //    base.OnNavigatedTo(parameters);

        //    if (parameters.ContainsKey("Balance"))
        //    {
        //        balance = parameters.GetValue<Balance>("Balance");
        //    }
        //}
    }
}