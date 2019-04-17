using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using Prism.Mvvm;
using Prism.Navigation;
using SADM.Extensions;
using SADM.Helpers;
using SADM.Models.Requests;
using SADM.Models.Responses;
using SADM.Services;
using SADM.Views;
using Xamarin.Forms;

namespace SADM.ViewModels
{
    /// <summary>
    /// Base from all ViewModels.
    /// </summary>
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        protected int notificationCounter;
        public int NotificationCounter { get => notificationCounter; set => SetProperty(ref notificationCounter, value); }

        protected bool isBusy;
        protected bool isConnected;
        protected INavigationService NavigationService { get; private set; }
        protected ISettingsService SettingsService { get; private set; }
        protected IHudService HudService { get; private set; }
        protected ISadmApiService ApiService { get; private set; }
        protected string leftIcon;
        protected string rightIcon;
        public string LeftIcon { get => leftIcon; set => SetProperty(ref leftIcon, value); }
        public string RightIcon { get => rightIcon; set => SetProperty(ref rightIcon, value); }
        public ICommand LeftIconCommand { get; set; }
        public ICommand RightIconCommand { get; set; }

        public bool IsConnected { get => isConnected; set => SetProperty(ref isConnected, value); }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:SADM.ViewModels.ViewModelBase"/> is busy.
        /// </summary>
        /// <value><c>true</c> if is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy { get => isBusy; set => SetProperty(ref isBusy, value); }

        public ViewModelBase(INavigationService navigationService, 
                             ISettingsService settingsService, 
                             IHudService hudService,
                             ISadmApiService apiService)
        {
            NavigationService = navigationService;
            SettingsService = settingsService;
            HudService = hudService;
            ApiService = apiService;
            LoadAppBar();
            IsConnected = Plugin.Connectivity.CrossConnectivity.Current.IsConnected;
            Plugin.Connectivity.CrossConnectivity.Current.ConnectivityChanged += (sender, args) => IsConnected = args.IsConnected;
            notificationCounter = 0;
        }

        protected void LoadAppBar()
        {
            if(!NeedIcons())
            {
                //No add icons
            }
            else if(IsMenuLateralOption())
            {
                LeftIcon = "menu.png";
                LeftIconCommand = new AsyncCommand(ShowLateralMenuAsync);
                RightIcon = "notificationoff.png";
                RightIconCommand = new AsyncCommand(GoToPageAsync<NotificationsPage>);
            }
            else if(!IsRootPage())
            {
                LeftIcon = "ic_back.png";
                LeftIconCommand = new AsyncCommand(NavigationService.GoBackAsync);
            }
            else
            {
                //forced exception to not forget to configure the icons in the new ViewModels
                throw new NotImplementedException($"You have to configure the ViewModel:{GetType().Name} icons in the ViewModelBase class");
            }
        }

        /// <summary>
        /// Ons the navigated from.
        /// </summary>
        /// <param name="parameters">Parameters.</param>
        public virtual void OnNavigatedFrom(Prism.Navigation.NavigationParameters parameters)
        {
            //Intentionally empty for the ViewModels to implement it
        }

        /// <summary>
        /// Ons the navigated to.
        /// </summary>
        /// <param name="parameters">Parameters.</param>
        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
            IsLateralMenuEnabled = IsMenuLateralOption();
        }

        /// <summary>
        /// Ons the navigating to.
        /// </summary>
        /// <param name="parameters">Parameters.</param>
        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {
            //Intentionally empty for the ViewModels to implement it
        }

        /// <summary>
        /// Destroy this instance.
        /// </summary>
        public virtual void Destroy()
        {
            //Intentionally empty for the ViewModels to implement it
        }

        public bool IsMenuLateralOption()
        {
            switch (GetType().Name)
            {
                case nameof(LateralMenuViewModel):
                case nameof(BalancesViewModel):
                case nameof(ServicesViewModel):
                case nameof(ReceiptsViewModel):
                case nameof(MyDataViewModel):
                case nameof(PaymentCenterListViewModel):
                case nameof(ContactViewModel):
                    return true;
                default:
                    return false;
            }
        }

        public bool IsRootPage()
        {
            switch (GetType().Name)
            {
                case nameof(SignUpViewModel):
                case nameof(AssociateContractViewModel):
                case nameof(NotificationsViewModel):
                case nameof(NotificationViewModel):
                case nameof(BalanceViewModel):
                case nameof(ReportListViewModel):
                case nameof(GenerateReportViewModel):
                case nameof(PayViewModel):
                    return false;
                default:
                    return false;
            }
        }

        public bool NeedIcons()
        {
            return !(GetType().Name == nameof(LoginViewModel) || 
                     GetType().Name == nameof(LateralMenuViewModel));
        }

        public async Task ShowLateralMenuAsync()
        {
            if (Application.Current.MainPage is MasterDetailPage masterDetailPage)
            {
                masterDetailPage.IsPresented = true;
                await Task.Delay(100);
            }
        }

        public bool HideLateralMenu()
        {
            if (Application.Current.MainPage is MasterDetailPage masterDetailPage)
            {
                masterDetailPage.IsPresented = false;
                return true;
            }
            return false;
        }

        public async Task HideLateralMenuAsync()
        {
            if(HideLateralMenu())
            {
                await Task.Delay(100);
            }
        }

        public bool IsLateralMenuEnabled
        {
            get => Application.Current.MainPage is MasterDetailPage masterDetailPage && masterDetailPage.IsGestureEnabled;
            set
            {
                if (Application.Current.MainPage is MasterDetailPage masterDetailPage)
                {
                    masterDetailPage.IsGestureEnabled = value;
                }
            }
        }

        public async Task GoToPageAsync<T>(object parameter = null) where T : Page
        {
            var navigationParameters = new NavigationParameters { { string.Empty, parameter } };
            var pageName = typeof(T).Name;
            switch (pageName)
            {
                case nameof(LoginPage):
                    await NavigationService.NavigateAsync($"myapp:///NavigationPage/{pageName}");
                    break;
                case nameof(LateralMenuPage):
                    await NavigationService.NavigateAsync(new Uri($"/{pageName}/NavigationPage/{nameof(BalancesPage)}", UriKind.Absolute));
                    break;
                //Lateral Menu Optios
                case nameof(MyDataPage):
                case nameof(BalancesPage):
                case nameof(ReceiptsPage):
                case nameof(ServicesPage):
                case nameof(PaymentCenterListPage):
                case nameof(ContactPage):
                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        HideLateralMenu();
                    }
                    await NavigationService.NavigateAsync(new Uri($"NavigationPage/{pageName}", UriKind.Relative));
                    break;
                //All others
                default:
                    await NavigationService.NavigateAsync(pageName, new NavigationParameters { { string.Empty, parameter } });
                    break;
            }
        }

        protected async Task<V> CallServiceAsync<U, V>(U request, string progressMessage, bool showError) where U : RequestBase where V : ResponseBase, new()
        {
            var response = new V();
            if (!Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
            {
                response.AddError("Revisa tu conexión a internet y vuelve a intentarlo.");
            }
            else
            {
                response.ErrorList = request.GetErrorList();
                if (response.Success)
                {
                    HudService.ShowProgress(progressMessage);
                    response = await ApiService.CallServiceAsync<U, V>(request);
                    string json = JsonConvert.SerializeObject(request);
                    await HudService.HideProgressAsync();
                }
            }
            if (showError && !response.Success)
            {
                await HudService.ShowErrorListAsync(response.ErrorList);
            }
            return response;
        }
    }
}
