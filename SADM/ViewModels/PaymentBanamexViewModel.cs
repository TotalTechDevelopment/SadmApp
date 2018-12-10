
using Prism.Navigation;
using SADM.Helpers;
using SADM.Models;
using SADM.Models.Requests;
using SADM.Models.Responses;
using SADM.Services;
using System.Threading.Tasks;

namespace SADM.ViewModels
{
    public class PaymentBanamexViewModel : ViewModelBase
    {

        #region Vars 
        private DataCardModel card;
        protected ISadmApiService sadmApiService;
        #endregion

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

        public PaymentBanamexViewModel(ISadmApiService sadmApiService, INavigationService navigationService, ISettingsService settingsService, IHudService hudService, ISadmApiService apiService) : base(navigationService, settingsService, hudService, apiService)
        {
            this.sadmApiService = sadmApiService;
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            card = parameters.GetValue<DataCardModel>(string.Empty);
            VPCRequest conn = new VPCRequest();
            conn.AddDigitalOrderField("vpc_Version", SADM.Settings.AppConfiguration.Values.vpc_Version);
            conn.AddDigitalOrderField("vpc_Command", SADM.Settings.AppConfiguration.Values.vpc_Command);
            conn.AddDigitalOrderField("vpc_AccessCode", SADM.Settings.AppConfiguration.Values.vpc_AccessCode);
            conn.AddDigitalOrderField("vpc_Merchant", SADM.Settings.AppConfiguration.Values.vpc_Merchant);
            conn.AddDigitalOrderField("vpc_ReturnURL", "http://localhost:8080/api/");
            conn.AddDigitalOrderField("vpc_MerchTxnRef", "PruebaRfId2529");
            conn.AddDigitalOrderField("vpc_OrderInfo", "2529");
            conn.AddDigitalOrderField("vpc_Amount", (card.Amount * 100).ToString());
            conn.AddDigitalOrderField("vpc_Currency", SADM.Settings.AppConfiguration.Values.vpc_Currency);
            //conn.AddDigitalOrderField("vpc_CustomPaymentPlanPlanId", vpc_CustomPaymentPlanPlanId.Text);
            conn.AddDigitalOrderField("vpc_Locale", SADM.Settings.AppConfiguration.Values.vpc_Locale);
            // Perform the transaction
            string url = conn.Create3PartyQueryString();
            url = "https://banamex.dialectpayments.com/vpcpay" + url;
            UrlWeb = url;
        }

    }
}
