
using Prism.Navigation;
using SADM.Models;
using SADM.Services;

namespace SADM.ViewModels
{
    public class PaymentBanamexViewModel : ViewModelBase
    {

        #region Vars 
        private DataCardModel card;
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

        public PaymentBanamexViewModel(INavigationService navigationService, ISettingsService settingsService, IHudService hudService, ISadmApiService apiService) : base(navigationService, settingsService, hudService, apiService)
        {
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if(parameters.ContainsKey(string.Empty))
            {
                card = parameters.GetValue<DataCardModel>(string.Empty);
                UrlWeb = $"https://banamex.dialectpayments.com/vpcpay?vpc_Amount={card.Amount}&vpc_Version=1&vpc_OrderInfo=50000008&vpc_Command=pay&vpc_Currency=MXN&vpc_Merchant=TEST1008073&vpc_ReturnAuthResponseData=N&vpc_ReturnURL=http%3A%2F%2Flocalhost%3A8080%2FaydB%2FpaymentController&vpc_SecureHash=DE45F717A8C5A4BB6C7AA205BF4CEC9054D6627CD757C535C54AA78B7E290F23&vpc_SecureHashType=SHA256&vpc_AccessCode=B3B33E35&vpc_MerchTxnRef=67ff3b50%3A16659de4b9d%3A-8000&vpc_Locale=es_MX";
            }
        }

    }
}
