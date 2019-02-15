using System.Threading.Tasks;
using Prism.Navigation;
using SADM.Helpers;
using SADM.Models;
using SADM.Models.Requests;
using SADM.Models.Responses;
using SADM.Services;
using SADM.Views;
using System;
using SADM.Controls;
using System.Linq;

namespace SADM.ViewModels
{
    public class AssociateContractViewModel : ViewModelBase
    {
        public const string PARAMETER_SIGNUP = "data";
        protected const int NIS_LENGTH = 18;
        protected const int INDEX_OF_NIS = 0;

        protected SignUpRequest request;
        protected bool isRegister;
        protected string nis;
        protected string previousReading;
        protected string title;
        protected string action;

        public string Nis { get => nis; set => SetProperty(ref nis, value); }
        public string Title { get => title; set => SetProperty(ref title, value); }
        public string Action { get => action; set => SetProperty(ref action, value); }
        public string PreviousReading { get => previousReading; set => SetProperty(ref previousReading, value); }

        ObservableCollectionExt<Balance> BalanceList;

        public IAsyncCommand ReadBarCodeForNisCommand { get; private set; }
        public IAsyncCommand ShowInfoForNisCommand { get; private set; }
        public IAsyncCommand AssociateContractAttemptCommand { get; private set; }
        public IAsyncCommand RemoveContractAttemptCommand { get; private set; }

        public AssociateContractViewModel(INavigationService navigationService, 
                                          ISettingsService settingsService, 
                                          IHudService hudService, 
                                          ISadmApiService apiService) : 
        base(navigationService, settingsService, hudService, apiService)
        {
            ReadBarCodeForNisCommand = new AsyncCommand(ReadBarCodeForNisAsync);
            ShowInfoForNisCommand = new AsyncCommand(() => HudService.ShowExampleAsync(AppResources.NisHelp, "factura.png"));
            AssociateContractAttemptCommand = new AsyncCommand(AssociateContractAttemptAsync);
            RemoveContractAttemptCommand = new AsyncCommand(RemoveContractAttemptAsync);

            BalanceList = new ObservableCollectionExt<Balance>();
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            if (parameters.ContainsKey(string.Empty) && parameters.GetValue<object>(string.Empty) is ObservableCollectionExt<Balance>)
            {
                BalanceList = parameters.GetValue<ObservableCollectionExt<Balance>>(string.Empty) as ObservableCollectionExt<Balance>;
                isRegister = false;
                Title = AppResources.AssociateContractTitle;
                Action = AppResources.AddContract;
            }
            else
            {
                if (parameters.ContainsKey(string.Empty) && parameters.GetValue<SignUpRequest>(string.Empty) is SignUpRequest data)
                {
                    isRegister = true;
                    request = data;
                    Title = AppResources.SignUpTitle;
                    Action = AppResources.AssociateContractButton;
                }
                else
                {
                    isRegister = false;
                    Title = AppResources.AssociateContractTitle;
                    Action = AppResources.AddContract;
                }
            }
        }

        protected async Task ReadBarCodeForNisAsync()
        {
            var barCode = await ScanBarCodeAsync();
            Nis = !string.IsNullOrEmpty(barCode) && barCode.Length > INDEX_OF_NIS + NIS_LENGTH ?
                         barCode.Substring(INDEX_OF_NIS, NIS_LENGTH) : string.Empty;
        }

        bool ValidateBarCode(string barCode)
        {

            if(barCode.Length >= 18)
            {
                var firstPart = barCode.Substring(0, 10);
                var dd = barCode.Substring(10, 2);
                var mm = barCode.Substring(12, 2);
                var yyyy = barCode.Substring(14, 4);
                var lastPart = barCode.Substring(18, barCode.Length - 18);
                var barCodeFixed = $"{firstPart}{yyyy}{mm}{dd}{lastPart}";
                var sum = 0;
                for (var i = default(int); i < 18; i++)
                {
                    sum += (i+1) * int.Parse(barCodeFixed.Substring(i, 1));
                }
                var first = sum % 11;

                while(lastPart.Length < 16)
                {
                    lastPart = $"0{lastPart}";
                }
                lastPart = $"{first}{lastPart}";
                sum = 0;
                for (var i = default(int); i < 15; i++)
                {
                    sum += (i + 1) * int.Parse(lastPart.Substring(i, 1));
                }
                var second = sum % 11;

            }
            return false;
        }

        protected async Task AssociateContractAttemptAsync()
        {
            if(string.IsNullOrEmpty(Nis))
            {
                await HudService.ShowErrorAsync("El NIR no fue ingresado, ingresar NIR.");
                return;
            }
            if (Nis.Contains('-') && Nis.Split('-').Count() == 4)
            {
                var arrayNIS = Nis.Split('-');
                bool isValidNis = true;
                foreach (var item in arrayNIS)
                {
                    if (!int.TryParse(item, out int value))
                    {
                        var dateNisArray = item.Split('/');
                        if (dateNisArray.Count() == 3)
                        {
                            foreach (var itemDate in dateNisArray)
                            {
                                isValidNis &= int.TryParse(itemDate, out int valu);
                            }
                        }
                        else
                        {
                            isValidNis = false;
                        }
                    }
                }
                if(!isValidNis)
                {
                    await HudService.ShowErrorAsync("El NIR ingresado no es válido debe contener solo dígitos, intente con otro.");
                    return;
                }
                if (BalanceList.Where(x => x.Nis == arrayNIS[1]).Any())
                {
                    //Nir = DecodeNis(Nis),
                    //PreviousReading = PreviousReading,
                    await HudService.ShowErrorAsync("El NIR ingresado ya existe, intente con otro.");
                    return;
                }
            }
            else
            {
                await HudService.ShowErrorAsync("El NIR ingresado no es válido, intente con otro.");
                return;
            }
            if(PreviousReading is null)
            {
                await HudService.ShowErrorAsync("La lectura anterior no fue ingresado, ingresar lectura anterior.");
                return;
            }
            if (!int.TryParse(PreviousReading, out int val) && PreviousReading.Count() < 4)
            {
                await HudService.ShowErrorAsync("La lectura anterior no es valido debe estar compuesta de 4 dígitos, intente con otro.");
                return;
            }
            if (isRegister)
            {
                request.Nir = Nis;// DecodeNis(Nis);
                request.PreviousReading = PreviousReading;
                if (await CallServiceAsync<SignUpRequest, SignUpResponse>(request, AppResources.SignUpLoading, true) is SignUpResponse response && response.Success)
                {
                    var newUser = new User { 
                        Name = request.Name,
                        LastName = request.LastName,
                        SecondLastName = request.SecondLastName,
                        Folio = long.Parse(response.Token),
                        Email = request.Email,
                        IsActive = request.Active,
                        Street = request.Street,
                        Number = request.Number,
                        Colony = request.Colony,
                        City = request.City,
                        State = request.State,
                        PostalCode = request.PostalCode,
                        PhoneNumber = (request.PhoneNumber).ToString(),
                        Question = request.Question,
                        Answer = request.Answer,
                        Password = request.Password
                    };
                    await SettingsService.WriteSessionDataAsync(newUser, newUser.Email, true);
                    await GoToPageAsync<LateralMenuPage>();
                }
            }
            else
            {
                var addContractRequest = new AddContractRequest {
                    Nir = Nis, //DecodeNis(Nis),
                    PreviousReading = PreviousReading,
                    Email = SettingsService.User.Email,
                    UserId = SettingsService.User.Folio
                };
                if(await CallServiceAsync<AddContractRequest, AddContractResponse>(addContractRequest, AppResources.AddContractLoading, true) 
                   is AddContractResponse response && response.Success)
                {
                    await NavigationService.GoBackAsync();
                }
            }
        }

        protected async Task RemoveContractAttemptAsync()
        {
            if (isRegister)
            {
                request.Nir = Nis;// DecodeNis(Nis);
                request.PreviousReading = PreviousReading;
                if (await CallServiceAsync<SignUpRequest, SignUpResponse>(request, AppResources.SignUpLoading, true) is SignUpResponse response && response.Success)
                {
                    var newUser = new User
                    {
                        Name = request.Name,
                        LastName = request.LastName,
                        SecondLastName = request.SecondLastName,
                        Folio = long.Parse(response.Token),
                        Email = request.Email,
                        IsActive = request.Active,
                        Street = request.Street,
                        Number = request.Number,
                        Colony = request.Colony,
                        City = request.City,
                        State = request.State,
                        PostalCode = request.PostalCode,
                        PhoneNumber = (request.PhoneNumber).ToString(),
                        Question = request.Question,
                        Answer = request.Answer,
                        Password = request.Password
                    };
                    await SettingsService.WriteSessionDataAsync(newUser, newUser.Email, true);
                    await GoToPageAsync<LateralMenuPage>();
                }
            }
            else
            {
                var removeContractRequest = new RemoveContractRequest
                {
                    Nir = Nis,//DecodeNis(Nis),
                    PreviousReading = PreviousReading,
                    Email = SettingsService.User.Email,
                    UserId = SettingsService.User.Folio
                };
                if (await CallServiceAsync<RemoveContractRequest, RemoveContractResponse>(removeContractRequest, AppResources.AddContractLoading, true)
                   is RemoveContractResponse response && response.Success)
                {
                    await NavigationService.GoBackAsync();
                }
            }
        }

        private string DecodeNis(string _nis){
            var result = _nis.Replace("-","").Replace("/","");
            return result;
        }
        protected async Task<string> ScanBarCodeAsync()
        {
            var barCode = string.Empty;
            var scanner = new ZXing.Mobile.MobileBarcodeScanner();
            var result = await scanner.Scan();
            barCode = result?.Text;
            return barCode;
        }
    }
}
