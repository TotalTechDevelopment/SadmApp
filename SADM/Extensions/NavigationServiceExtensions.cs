using System;
using System.Threading.Tasks;
using Prism.Navigation;
using SADM.Views;
using Xamarin.Forms;

namespace SADM.Extensions
{
    public static class NavigationServiceExtensions
    {
        /*public static async Task InitAsync(this INavigationService navigationService, Services.ISettingsService settingsService)
        {
            if (string.IsNullOrWhiteSpace(settingsService.SessionToken))
            {
                await navigationService.GoToPageAsync<LoginPage>();
            }
            else
            {
                await navigationService.GoToPageAsync<LateralMenuPage>();
            }
        }

        public static async Task GoToPageAsync<T>(this INavigationService navigationService) where T : Page
        {
            await navigationService.GoToPageAsync<T>(null);
        }

        public static async Task GoToPageAsync<T>(this INavigationService navigationService, object parameter = null) where T : Page
        {
            var navigationParameters = new NavigationParameters{{string.Empty, parameter}};
            var pageName = typeof(T).Name;
            switch (pageName)
            {
                case nameof(LoginPage):
                    await navigationService.NavigateAsync($"myapp:///NavigationPage/{pageName}");
                    break;
                case nameof(LateralMenuPage):
                    await navigationService.NavigateAsync(new Uri($"/{pageName}/NavigationPage/{nameof(BalancesPage)}", UriKind.Absolute));
                    break;
                //Lateral Menu Optios
                case nameof(MyDataPage):
                case nameof(BalancesPage):
                case nameof(ReceiptsPage):
                case nameof(ServicesPage):
                case nameof(PaymentCenterListPage):
                case nameof(ContactPage):
                    await navigationService.NavigateAsync(new Uri($"NavigationPage/{pageName}", UriKind.Relative));
                    break;
                //All others
                default:
                    await navigationService.NavigateAsync(pageName, new NavigationParameters { { string.Empty, parameter } });
                    break;
            }
        }*/
    }
}
