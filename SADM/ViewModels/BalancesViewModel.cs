using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using SADM.Controls;
using SADM.Helpers;
using SADM.Models;
using SADM.Models.Requests;
using SADM.Models.Responses;
using SADM.Services;
using Xamarin.Forms;

namespace SADM.ViewModels
{
    public class BalancesViewModel : ViewModelBase
    {

        protected bool showPaymentButton;
        protected bool showDeleteButton;
        public bool ShowPaymentButton { get => showPaymentButton; set => SetProperty(ref showPaymentButton, value); }
        public bool ShowDeleteButton { get => showDeleteButton; set => SetProperty(ref showDeleteButton, value); }
        protected bool loading;
        public bool Loading { get => loading; set => SetProperty(ref loading, value); }
        public ObservableCollectionExt<Balance> BalanceList { get; private set; }

        public IAsyncCommand SeeMoreCommand { get; private set;}
        public ICommand ItemSelectedCommand => new Command(OnSelected);
        public IAsyncCommand PaymentCommand => new AsyncCommand(PaymentAsync);
        public IAsyncCommand DeleteCommand => new AsyncCommand(DeleteAsync);
        public IAsyncCommand AddContractCommand => new AsyncCommand(GoToPageAsync<Views.AssociateContractPage>);

        public BalancesViewModel(INavigationService navigationService,
                                 ISettingsService settingsService,
                                 IHudService hudService,
                                 ISadmApiService apiService) :
        base(navigationService, settingsService, hudService, apiService)
        {
            BalanceList = new ObservableCollectionExt<Balance>();
            SeeMoreCommand = new AsyncCommand(SeeMoreAsync);
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            new Command(async () => await GetBalanceList()).Execute(null);
        }

        protected async Task SeeMoreAsync(object arg)
        {
            if(arg is Balance balance)
            {
                await GoToPageAsync<Views.BalancePage>(balance);
            }
        }

        protected async Task GetBalanceList()
        {
            Loading = true;
            var request = new GetContractListRequest { Email = SettingsService.User.Email };
            if(await CallServiceAsync<GetContractListRequest, GetBalanceListResponse>(request, null, false) is GetBalanceListResponse response && response.Success)
            {
                BalanceList.Reset(response.BalanceList);
            }
            Loading = false;
        }

        protected void OnSelected(object obj)
        {
            if (obj is Balance balanceSelected)
            {
                balanceSelected.Selected = !balanceSelected.Selected;
            }
            // ShowPaymentButton = BalanceList.Any((balance) => balance.Selected);
            ShowDeleteButton = BalanceList.Any((balance) => balance.Selected);
            BalanceList.Refresh();
        }

        protected async Task PaymentAsync()
        {
            await HudService.ShowErrorAsync("Servicio no disponible por el momento.");
            BalanceList.Refresh();
            ShowPaymentButton = false;
        }

        protected async Task DeleteAsync()
        {
       
            await DeleteBalanceAsync();

        }


        protected async Task DeleteBalanceAsync()
        {
            await HideLateralMenuAsync();
            if (await HudService.ShowQuestionAsync(AppResources.DeleteBalanceTitle,
                                                  AppResources.DeleteBalanceMessage,
                                                  AppResources.CloseSessionAcceptButton,
                                                  AppResources.CloseSessionCancelButton))
            {
                foreach (var balance in BalanceList)
                {
                    if (balance.Selected)
                    {
                        await RemoveContractAttemptAsync(balance);
                    }
                }

                await GetBalanceList();
                BalanceList.Refresh();
                ShowDeleteButton = BalanceList.Any((balance) => balance.Selected);
            

            }
        }

        protected async Task RemoveContractAttemptAsync(Balance balance)
        {
            var removeContractRequest = new RemoveContractRequest
                {
                    Nir = "0",
                    PreviousReading = "0",
                    Email = SettingsService.User.Email,
                    UserId = SettingsService.User.Folio,
                    Nis = balance.Nis,
                    NisRad=balance.Nis,
                    SecRec="0",

                };

             if (await CallServiceAsync<RemoveContractRequest, RemoveContractResponse>(removeContractRequest, AppResources.DeleteContractLoading, true)
                   is RemoveContractResponse response && response.Success)
                {


            }

        }



    }
}
