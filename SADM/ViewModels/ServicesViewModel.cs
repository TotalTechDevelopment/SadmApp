using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Prism.Navigation;
using SADM.Helpers;
using SADM.Models;
using SADM.Models.Requests;
using SADM.Models.Responses;
using SADM.Services;

namespace SADM.ViewModels
{
    public class ServicesViewModel : ViewModelBase
    {
        public ObservableCollection<SadmService> ServiceList { get; private set; }
        public IAsyncCommand ShowServiceCommand { get; private set; }

        public ServicesViewModel(INavigationService navigationService, 
                                 ISettingsService settingsService, 
                                 IHudService hudService, 
                                 ISadmApiService apiService) : 
        base(navigationService, settingsService, hudService, apiService)
        {
            ShowServiceCommand = new AsyncCommand(ShowServiceAsync);
            ServiceList = new ObservableCollection<SadmService>
            {
                new SadmService
                {
                    Icon = "document.png",
                    Name = AppResources.GenerateReport.ToUpper(),
                    Description = AppResources.GenerateReportDesciption
                },
                new SadmService
                {
                    Icon = "bigclock.png",
                    Name = AppResources.RequestStatus.ToUpper(),
                    Description = AppResources.RequestStatusDescription
                }
            };
        }

        protected async Task ShowServiceAsync(object serviceSelected)
        {
            if (serviceSelected is SadmService service)
            {
                if (service.Name.ToUpper() == AppResources.GenerateReport.ToUpper())
                {
                    await GetNisListAsync();
                }
                else
                {
                    await GetReportListAsync();
                }
            }
        }

        public async Task GetNisListAsync()
        {
            var request = new GetContractListRequest { Email = SettingsService.User.Email };
            if (await CallServiceAsync<GetContractListRequest, GetBalanceListResponse>(request, "Preparando la generación de reporte...", true) is GetBalanceListResponse response && response.Success)
            {
                if(response.BalanceList.Any())
                {
                    await GoToPageAsync<Views.GenerateReportPage>(response.BalanceList);
                }
                else
                {
                    await HudService.ShowErrorAsync("Debes tener al menos un NIS registrado");
                }
            }
        }

        public async Task GetReportListAsync()
        {
            RegistroUsuariosGetAllRequest requests = new RegistroUsuariosGetAllRequest
            {
                order = "  Registro_de_Usuarios.Folio ASC ",
                where = "Registro_de_Usuarios.Correo='" + SettingsService.User.Email + "'"
            };
            if (await CallServiceAsync<RegistroUsuariosGetAllRequest, LoginResponse>(requests, "", false) is LoginResponse responseLogin && responseLogin.Success)
            {
                var request = new GetReportListRequest { UserId = responseLogin.Folio };
                if (await CallServiceAsync<GetReportListRequest, GetReportListResponse>(request, "Consultando las solicitudes...", true) is GetReportListResponse response && response.Success)
                {
                    if (response.ReportList.Any())
                    {
                        await GoToPageAsync<Views.ReportListPage>(response.ReportList);
                    }
                    else
                    {
                        await HudService.ShowErrorAsync("No has generado solicitudes");
                    }
                }
            }
        }
    }
}