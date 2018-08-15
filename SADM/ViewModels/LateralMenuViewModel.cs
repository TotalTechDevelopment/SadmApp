using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Prism.Navigation;
using SADM.Helpers;
using SADM.Services;
using SADM.Views;

namespace SADM.ViewModels
{
    public class LateralMenuViewModel : ViewModelBase
    {
        protected string userName;
        public string UserName { get => userName; set => SetProperty(ref userName, value); }
        public ObservableCollection<Tuple<string, string, Func<object, Task>>> MenuOptionList { get; private set; }
        public IAsyncCommand OptionSelectedCommand { get; private set; }
        public IAsyncCommand CloseSessionCommand { get; private set; }

        public LateralMenuViewModel(INavigationService navigationService, 
                                    ISettingsService settingsService, 
                                    IHudService hudService, 
                                    ISadmApiService apiService) : 
        base(navigationService, settingsService, hudService, apiService)
        {
            UserName = $"{SettingsService.User.Name} {SettingsService.User.LastName ?? string.Empty}";
            MenuOptionList = new ObservableCollection<Tuple<string, string, Func<object, Task>>>
            {
                new Tuple<string, string, Func<object, Task>>("user.png", AppResources.MenuMyData, GoToPageAsync<MyDataPage>),
                new Tuple<string, string, Func<object, Task>>("money.png", AppResources.MenuMyBalances, GoToPageAsync<BalancesPage>),
                new Tuple<string, string, Func<object, Task>>("receipt.png", AppResources.MenuBills, GoToPageAsync<ReceiptsPage>),
                new Tuple<string, string, Func<object, Task>>("graph.png", AppResources.MenuReports, GoToPageAsync<ServicesPage>),
                new Tuple<string, string, Func<object, Task>>("location.png", AppResources.MenuPaymentCenter, GoToPageAsync<PaymentCenterListPage>),
                new Tuple<string, string, Func<object, Task>>("phone.png", AppResources.MenuContact, GoToPageAsync<ContactPage>)
            };
            OptionSelectedCommand = new AsyncCommand(OnOptionSelectedAsync);
            CloseSessionCommand =   new AsyncCommand(CloseSessionAsync);
        }

        /// <summary>
        /// Navigate to Page Selected from LateralMenu.
        /// </summary>
        /// <returns>No object or value is returned by this method when it completes.</returns>
        /// <param name="itemSelected">Item selected.</param>
        protected async Task OnOptionSelectedAsync(object itemSelected)
        {
            if(itemSelected is Tuple<string, string, Func<object, Task>> optionSelected)
            {
                await optionSelected.Item3?.Invoke(null);
            }
        }

        /// <summary>
        /// Closes the session.
        /// </summary>
        /// <returns>No object or value is returned by this method when it completes.</returns>
        protected async Task CloseSessionAsync()
        {
            await HideLateralMenuAsync();
            if(await HudService.ShowQuestionAsync(AppResources.CloseSessionTitle, 
                                                  AppResources.CloseSessionMessage, 
                                                  AppResources.CloseSessionAcceptButton, 
                                                  AppResources.CloseSessionCancelButton))
            {
                await SettingsService.WriteUserAsync(null);
                await GoToPageAsync<LoginPage>();
            }
        }
    }
}
