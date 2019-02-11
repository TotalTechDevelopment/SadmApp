using Prism;
using Prism.Ioc;
using SADM.ViewModels;
using SADM.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.DryIoc;
using SADM.Services;
using System;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SADM
{
    public partial class App : PrismApplication
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SADM.App"/> class.
        /// </summary>
        public static App CurrentInstance { get; private set; }
        public App() : this(null) 
        {
            //Force constructor for PRISM
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SADM.App"/> class.
        /// </summary>
        /// <param name="initializer">Initializer.</param>
        public App(IPlatformInitializer initializer) : base(initializer) 
        { 
            //Constructor for PRISM
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            CurrentInstance = this;
            var navigationService = Container.Resolve<Prism.Navigation.INavigationService>();
            if(Container.Resolve<ISettingsService>().User is null)
            {
                await navigationService.NavigateAsync($"myapp:///{nameof(NavigationPage)}/{nameof(LoginPage)}");
            }
            else
            {
                await navigationService.NavigateAsync(new Uri($"/{nameof(LateralMenuPage)}/{nameof(NavigationPage)}/{nameof(BalancesPage)}", UriKind.Absolute));
            }
        }

        /// <summary>
        /// Registers the Views, ViewModels and Services.
        /// </summary>
        /// <param name="containerRegistry">Container registry.</param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Views & ViewModels
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpViewModel>();
            containerRegistry.RegisterForNavigation<AssociateContractPage, AssociateContractViewModel>();
            containerRegistry.RegisterForNavigation<LateralMenuPage, LateralMenuViewModel>();
            containerRegistry.RegisterForNavigation<BalancesPage, BalancesViewModel>();
            containerRegistry.RegisterForNavigation<BalancePage, BalanceViewModel>();
            containerRegistry.RegisterForNavigation<ReceiptsPage, ReceiptsViewModel>();
            containerRegistry.RegisterForNavigation<ServicesPage, ServicesViewModel>();
            containerRegistry.RegisterForNavigation<GenerateReportPage, GenerateReportViewModel>();
            containerRegistry.RegisterForNavigation<ReportListPage, ReportListViewModel>();
            containerRegistry.RegisterForNavigation<PaymentCenterListPage, PaymentCenterListViewModel>();
            containerRegistry.RegisterForNavigation<NotificationsPage, NotificationsViewModel>();
            containerRegistry.RegisterForNavigation<NotificationPage, NotificationViewModel>();
            containerRegistry.RegisterForNavigation<MyDataPage, MyDataViewModel>();
            containerRegistry.RegisterForNavigation<PayPage, PayViewModel>();
            containerRegistry.RegisterForNavigation<ContactPage, ContactViewModel>();
            containerRegistry.RegisterForNavigation<PaymentBanamex, PaymentBanamexViewModel>();
            //Services
            containerRegistry.Register<ISettingsService, SettingsService>();
            containerRegistry.Register<ISadmApiService, SadmApiService>();
            containerRegistry.Register<IHudService, HudService>();
        }

        protected override void OnStart()
        {
            base.OnStart();
            AppCenter.Start("android=072fd3d1-473a-4e88-9cde-50c3877676cc;" +
                  "ios=64d5501f-2fe8-4296-868b-6f0c8fe90edc;",
                  typeof(Analytics), typeof(Crashes));
        }
    }
}
