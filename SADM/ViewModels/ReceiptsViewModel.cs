using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Prism.Navigation;
using SADM.Controls;
using SADM.Enums;
using SADM.Helpers;
using SADM.Models;
using SADM.Models.Requests;
using SADM.Models.Responses;
using SADM.Services;
using Xamarin.Forms;

namespace SADM.ViewModels
{
    public class ReceiptsViewModel : ViewModelBase
    {
        public IList<Bill> BillList { get; private set; }
        public ObservableCollectionExt<Balance> ContractList { get; private set; }
        protected bool loading;
        public bool Loading { get => loading; set => SetProperty(ref loading, value); }

        public ReceiptsViewModel(INavigationService navigationService, 
                                 ISettingsService settingsService, 
                                 IHudService hudService, 
                                 ISadmApiService apiService) : 
        base(navigationService, settingsService, hudService, apiService)
        {
            BillList = new List<Bill>();
            ContractList = new ObservableCollectionExt<Balance>();
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            new Command(async () => await GetBillListAsync()).Execute(null);
        }

        protected async Task GetBillListAsync()
        {
            Loading = true;
            var request = new GetBillListRequest { Email = SettingsService.User.Email };
            if (await CallServiceAsync<GetBillListRequest, GetBillListResponse>(request, null, false) is GetBillListResponse response && response.Success)
            {
                BillList = response.BillList;
            }
            var getContractListRequest = new GetContractListRequest { Email = SettingsService.User.Email };
            if (await CallServiceAsync<GetContractListRequest, GetBalanceListResponse>(getContractListRequest, null, false)
                is GetBalanceListResponse getBalanceListResponse && getBalanceListResponse.Success)
            {
                foreach (var contract in getBalanceListResponse.BalanceList)
                {
                    contract.BillDeliveryConfiguration = (BillDeliveryConfiguration)contract.V_Envio;
                    contract.DownloadCommand = new AsyncCommand(async () => await DownloadAsync(contract));
                    contract.SendToTheAddressCommand = new AsyncCommand(async () => await ChangeConfigurationAsync(contract));
                    contract.SendToEmailCommand = new AsyncCommand(async () => await ChangeConfigurationAsync(contract));
                }
                ContractList.Reset(getBalanceListResponse.BalanceList);
            }
            Loading = false;
        }


        protected async Task DownloadAsync(Balance contract)
        {
            var billListOfNisSelected = BillList?.Where(b => b.Nis == contract.Nis).ToList();
            if(billListOfNisSelected == null || !billListOfNisSelected.Any())
            {
                await HudService.ShowErrorAsync("No hay archivos disponibles");
                return;
            }

            string text1 = null;
            string text2 = null;
            string text3 = null;
            if(billListOfNisSelected != null && billListOfNisSelected.Count >= 1)
            {
                text1 = billListOfNisSelected[0]?.BillDate;
            }
            if (billListOfNisSelected != null && billListOfNisSelected.Count >= 2 )
            {
                text2 = billListOfNisSelected[1]?.BillDate;
            }
            if (billListOfNisSelected != null && billListOfNisSelected.Count >= 3)
            {
                text3 = billListOfNisSelected[2]?.BillDate;
            }

            var result = await HudService.ShowDownloadBillAsync(contract.Nis, 
                                                                text1, text2, text3,
                                                                "Cancelar");
            var message = "Se inicio la descarga de la factura en formato ";
            var download = string.Empty;
            var fileName = $"factura_{contract.Nis}_";
            switch(result)
            {
                case Enums.DownloadBillPopupResult.FirstPdf:
                    download = billListOfNisSelected[0].Pdf;
                    fileName = $"{fileName}{billListOfNisSelected[0].BillDate.Replace(" ", string.Empty)}.pdf";
                    message = $"{message}PDF";
                    break;
                case Enums.DownloadBillPopupResult.FirstXml:
                    download = billListOfNisSelected[0].Xml;
                    fileName = $"{fileName}{billListOfNisSelected[0].BillDate.Replace(" ", string.Empty)}.xml";
                    message = $"{message}XML";
                    break;
                case Enums.DownloadBillPopupResult.SecondPdf:
                    download = billListOfNisSelected[1].Pdf;
                    fileName = $"{fileName}{billListOfNisSelected[1].BillDate.Replace(" ", string.Empty)}.pdf";
                    message = $"{message}PDF";
                    break;
                case Enums.DownloadBillPopupResult.SecondXml:
                    download = billListOfNisSelected[1].Xml;
                    fileName = $"{fileName}{billListOfNisSelected[1].BillDate.Replace(" ", string.Empty)}.xml";
                    message = $"{message}XML";
                    break;
                case Enums.DownloadBillPopupResult.ThirdPdf:
                    download = billListOfNisSelected[2].Pdf;
                    fileName = $"{fileName}{billListOfNisSelected[2].BillDate.Replace(" ", string.Empty)}.pdf";
                    message = $"{message}PDF";
                    break;
                case Enums.DownloadBillPopupResult.ThirdXml:
                    download = billListOfNisSelected[2].Xml;
                    fileName = $"{fileName}{billListOfNisSelected[2].BillDate.Replace(" ", string.Empty)}.xml";
                    message = $"{message}XML";
                    break;
                default:
                    message = null;
                    break;
            }
            if(!string.IsNullOrEmpty(message) && await CheckStoragePermissionAsync())
            {
                var asdf = DependencyService.Get<IDownloadService>();
                    asdf.DownloadFile(download, fileName);
                await HudService.ShowSuccessMessageAsync($"{message} del NIS:{contract.Nis}");
            }
        }

        protected async Task<bool> CheckStoragePermissionAsync()
        {
            
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
            if (status != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage))
                {
                    await HudService.ShowErrorAsync("Es necesario que habilites los permisos de almacenamiento");
                }

                var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
                if (results.ContainsKey(Permission.Storage)){
                    status = results[Permission.Storage];
                }   
            }
            if (status == PermissionStatus.Granted)
            {
                return true;
            }
            else if (status != PermissionStatus.Unknown)
            {
                await HudService.ShowErrorAsync("No se realizo la descarga debido a que rechazaste los permisos de almacenamiento.");
            }
            return false;
        }

        protected async Task ChangeConfigurationAsync(Balance contract)
        {
            var bdc = contract.BillDeliveryConfiguration;
            var request = new UpdateBillDeliveryConfigurationRequest { 
                Email = SettingsService.User.Email,
                Indicator = (int)bdc,
                Nis = contract.Nis
            };
            await CallServiceAsync<UpdateBillDeliveryConfigurationRequest, UpdateBillDeliveryConfigurationResponse>(request, "Cambiando la configuración...", true);
        }
    }
}