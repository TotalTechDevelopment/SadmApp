using System.Threading.Tasks;
using Plugin.Messaging;
using Prism.Navigation;
using SADM.Helpers;
using SADM.Services;

namespace SADM.ViewModels
{
    public class ContactViewModel : ViewModelBase
    {
        protected string address;
        public string Address { get => address; set => SetProperty(ref address, value); }

        public IAsyncCommand CallAttemptCommand { get; private set; }

        public ContactViewModel(INavigationService navigationService, 
                                ISettingsService settingsService, 
                                IHudService hudService, 
                                ISadmApiService apiService) : 
        base(navigationService, settingsService, hudService, apiService)
        {
            Address = Settings.AppConfiguration.Values.ContactAddress;
            CallAttemptCommand = new AsyncCommand(MakeCallAttenptAsync);
        }

        protected async Task MakeCallAttenptAsync()
        {
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            if (phoneDialer.CanMakePhoneCall)
            {
                phoneDialer.MakePhoneCall(Settings.AppConfiguration.Values.ContactPhone);
            }  
            else
            {
                await HudService.ShowErrorAsync($"Este dispositivo no puede realizar LLamadas, puede comunicarse desde cualquier otro dispositivo al número {Settings.AppConfiguration.Values.ContactPhone}");
            }
        }
    }
}