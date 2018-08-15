using Prism.Navigation;
using SADM.Enums;
using SADM.Helpers;
using SADM.Models;
using SADM.Services;

namespace SADM.ViewModels
{
    public class NotificationViewModel : ViewModelBase
    {
        protected Notification notification;

        public Notification Notification { get => notification; set => SetProperty(ref notification, value); }
        public IAsyncCommand CallCommand { get; private set; }

        public NotificationViewModel(INavigationService navigationService, 
                                     ISettingsService settingsService, 
                                     IHudService hudService, 
                                     ISadmApiService apiService) : 
        base(navigationService, settingsService, hudService, apiService)
        {
            CallCommand = new AsyncCommand(() => HudService.ShowErrorAsync("Llamando..."));
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            if (parameters.ContainsKey(string.Empty))
            {
                Notification = parameters.GetValue<Notification>(string.Empty);
            }
        }

    }
}
