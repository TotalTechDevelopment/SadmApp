using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Prism.Events;
using Prism.Navigation;
using SADM.Events;
using SADM.Extensions;
using SADM.Helpers;
using SADM.Models;
using SADM.Models.Requests;
using SADM.Services;
using SADM.Views;
using Xamarin.Forms;

namespace SADM.ViewModels
{
    public class PayViewModel : ViewModelBase
    {
        private Balance balance;
        protected ISadmApiService sadmApiService;
        private IEventAggregator _event;
        private ISettingsService _settingsService;
        private readonly IHudService _hudService;
        private readonly INavigationService _navigationService;
        #region Properties
        private Uri urlWeb;
        public Uri UrlWeb
        {
            get { return urlWeb; }
            set
            {
                SetProperty(ref urlWeb, value);
            }
        }
        #endregion

        public PayViewModel(IEventAggregator eventAggregator, IHudService hudService, ISadmApiService sadmApiService, INavigationService navigationService, ISettingsService settingsService, ISadmApiService apiService) : base(navigationService, settingsService, hudService, apiService)
        {
            _navigationService = navigationService;
            _event = eventAggregator;
            _hudService = hudService;
            this.sadmApiService = sadmApiService;
            _event.GetEvent<UrlChangeEvent>().Subscribe(ResponseUrlPayment);
        }
        private bool yaNO;
        private async void ResponseUrlPayment(string url)
        {
            if (yaNO)
                return;

            yaNO = true;
            if (url.Contains("back"))
            {
                await _navigationService.GoBackAsync();
            }
            else
            {
                var dic = GetParams(url);
                var EstatusPago = dic.Where(e => e.Key == "vpc_TxnResponseCode").ToList().First().Value;
                if (EstatusPago == "2")
                {
                   
                    var request = new GetContractListRequest { Email = DatosPago.email };
                    if (await CallServiceAsync<GetContractListRequest, Models.Responses.GetBalanceListResponse>(request, "Actualizando pago", true) is Models.Responses.GetBalanceListResponse response && response.Success)
                    {
                        var seleccionado = response.BalanceList.Where(t => t.Nis == DatosPago.NIS_RAD.ToString()).First();
                        var fecha = seleccionado.v_fecsec.Remove(seleccionado.v_fecsec.Length - 1);
                        fecha = new string(fecha.Skip(6).Take(2).ToArray()) + new string(fecha.Skip(4).Take(2).ToArray()) + new string(fecha.Take(4).ToArray());
                        DatosPago.NIS_RAD = int.Parse(seleccionado.Nis);
                        DatosPago.SEC_NIS = seleccionado.SecNis ?? 0;
                        DatosPago.F_FACT = fecha;
                        await sadmApiService.CallServiceAsync<PAGOSRequest, Models.Responses.ResponseBase>(new PAGOSRequest
                        {
                            v_f_fact = DatosPago.F_FACT,
                            v_importe = DatosPago.v_importe / 100,
                            v_nis_rad = DatosPago.NIS_RAD,
                            v_referencia = DatosPago.v_referencia,
                            v_sec_nis = DatosPago.SEC_NIS,
                            v_sec_rec = DatosPago.SEC_REC
                        });
                        await _hudService.ShowSuccessMessageAsync("Pago realizado con éxito.");

                        await _navigationService.NavigateAsync(new Uri($"/{nameof(LateralMenuPage)}/{nameof(NavigationPage)}/{nameof(BalancesPage)}", UriKind.Absolute));

                    }
                    else
                    {
                        await _hudService.ShowErrorAsync("Ocurrio un error en el pago intente de nuevo.");
                        await _navigationService.GoBackAsync();
                    }
                }
                else
                {
                    if (EstatusPago.Equals("C"))
                    {
                        await _navigationService.GoBackAsync();
                    }
                    else
                    {
                        await _hudService.ShowErrorAsync("Ocurrio un error en el pago intente de nuevo");
                        await _navigationService.GoBackAsync();
                    }

                }
            }

            //await this.sadmApiService.CallServiceAsync(new PAGOSRequest
            //{
            //    v_f_fact = DatosPago.F_FACT,
            //    v_importe = DatosPago.v_importe,
            //    v_nis_rad = DatosPago.NIS_RAD,
            //    v_referencia = DatosPago.v_referencia,
            //    v_sec_nis = DatosPago.SEC_NIS,
            //    v_sec_rec = DatosPago.SEC_REC
            //});
        }

        private Dictionary<string, string> GetParams(string uri)
        {
            var matches = Regex.Matches(uri, @"[\?&](([^&=]+)=([^&=#]*))", RegexOptions.Compiled);
            return matches.Cast<Match>().ToDictionary(
                m => Uri.UnescapeDataString(m.Groups[2].Value),
                m => Uri.UnescapeDataString(m.Groups[3].Value)
            );
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            var fechaConcatenada = DateTime.Now.Day.ToString().PadLeft(2, '0') + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Year.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0') + DateTime.Now.Millisecond.ToString().PadLeft(6, '0');
            if (parameters.ContainsKey("Balance"))
            {
                balance = parameters.GetValue<Balance>("Balance");
            }
            DatosPago.v_referencia = "RfId" + fechaConcatenada;
            DatosPago.v_importe = int.Parse((balance.TotalDebt * 100).Value.ToString());
            VPCRequest conn = new VPCRequest();
            conn.AddDigitalOrderField("vpc_Version", SADM.Settings.AppConfiguration.Values.vpc_Version);
            conn.AddDigitalOrderField("vpc_Command", SADM.Settings.AppConfiguration.Values.vpc_Command);
            conn.AddDigitalOrderField("vpc_AccessCode", SADM.Settings.AppConfiguration.Values.vpc_AccessCode);
            conn.AddDigitalOrderField("vpc_Merchant", SADM.Settings.AppConfiguration.Values.vpc_Merchant);
            conn.AddDigitalOrderField("vpc_ReturnURL", "http://spartane.com/pay");
            conn.AddDigitalOrderField("vpc_MerchTxnRef", "RfId" + fechaConcatenada);
            conn.AddDigitalOrderField("vpc_OrderInfo", fechaConcatenada);
            balance.TotalDebt = (float?)0.1;
            conn.AddDigitalOrderField("vpc_Amount", (balance.TotalDebt * 100).ToString());
            conn.AddDigitalOrderField("vpc_Currency", SADM.Settings.AppConfiguration.Values.vpc_Currency);
            //conn.AddDigitalOrderField("vpc_CustomPaymentPlanPlanId", vpc_CustomPaymentPlanPlanId.Text);
            conn.AddDigitalOrderField("vpc_Locale", SADM.Settings.AppConfiguration.Values.vpc_Locale);
            // Perform the transaction
            string url = conn.Create3PartyQueryString();
            url = SADM.Settings.AppConfiguration.Values.PaymentBaseUrl + url;
            UrlWeb = new Uri(url);
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