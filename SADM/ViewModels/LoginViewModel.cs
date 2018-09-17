using System.Threading.Tasks;
using Prism.Navigation;
using SADM.Helpers;
using SADM.Models.Requests;
using SADM.Models.Responses;
using SADM.Services;
using SADM.Views;

namespace SADM.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        protected string email;
        protected string password;
        protected bool rememberMe;

        public string Email { get => email; set => SetProperty(ref email, value); }
        public string Password { get => password; set => SetProperty(ref password, value); }
        public bool RememberMe { get => rememberMe; set => SetProperty(ref rememberMe, value); }

        public IAsyncCommand LoginAttemptCommand => new AsyncCommand(LoginAttemptAsync);
        public IAsyncCommand PasswordRecoveryAttemptCommand => new AsyncCommand(PasswordRecoveryAttemptAsync);
        public IAsyncCommand SignUpCommand => new AsyncCommand(GoToPageAsync<SignUpPage>);

        public LoginViewModel(INavigationService navigationService, ISettingsService settingsService, 
                              IHudService hudService, ISadmApiService apiService) 
            : base(navigationService, settingsService, hudService, apiService)
        {
            RememberMe = true;
            Email = SettingsService.Email;
            rememberMe = !(SettingsService.RememberMe is bool tmp && !tmp);
        }

        protected async Task LoginAttemptAsync()
        {
            var request = new LoginRequest { Email = Email, Password = Password};
            if(await CallServiceAsync<LoginRequest, LoginResponse>(request, AppResources.LoginLoading, true) 
               is LoginResponse loginResponse && loginResponse.Success)
            {
                await SettingsService.WriteSessionDataAsync(loginResponse.User, Email, RememberMe);
                await GoToPageAsync<LateralMenuPage>();
            }
        }

        protected async Task PasswordRecoveryAttemptAsync()
        {

            var emailResult = await HudService.ShowRecoverPasswordAsync(AppResources.RecoverPasswordTitle,
                          AppResources.RecoverPasswordMessage,
                          AppResources.CloseSessionAcceptButton,
                          AppResources.CloseSessionCancelButton);

            if (emailResult != "")
            {

                var recoverPasswordRequest = new RecoverPasswordRequest
                {
                    Email = emailResult

                };

                if (await CallServiceAsync<RecoverPasswordRequest, RecoverPasswordResponse>(recoverPasswordRequest, AppResources.SendingRecoverPasswordMessage, true)
                      is RecoverPasswordResponse response && response.Success)
                {
                    await HudService.ShowSuccessMessageAsync(AppResources.PasswordRecoverySuccessful);

                }


            }

            // await HudService.ShowErrorAsync("Servicio no disponible por el momento.");
                /*var request = new PasswordRecoveryRequest { Email = Email };
                if(await CallServiceAsync<PasswordRecoveryRequest, PasswordRecoveryResponse>(request, AppResources.PasswordRecoveryLoading, true) 
                   is PasswordRecoveryResponse response && response.Success)
                {
                    await HudService.ShowSuccessMessageAsync(AppResources.PasswordRecoverySuccessful);
                }*/
            }

        //protected async Task DeleteBalanceAsync()
        //{
        //    await HideLateralMenuAsync();
        //    if (await HudService.ShowQuestionAsync(AppResources.RecoverPasswordTitle,
        //                                          AppResources.RecoverPasswordMessage,
        //                                          AppResources.CloseSessionAcceptButton,
        //                                          AppResources.CloseSessionCancelButton))
        //    {
        //        foreach (var balance in BalanceList)
        //        {
        //            if (balance.Selected)
        //            {
        //                await RemoveContractAttemptAsync(balance);
        //            }
        //        }

        //        await GetBalanceList();
        //        BalanceList.Refresh();
        //        ShowDeleteButton = BalanceList.Any((balance) => balance.Selected);


        //    }
        //}

    }
}
