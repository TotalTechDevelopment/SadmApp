using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using SADM.Enums;
using SADM.Helpers;
using SADM.Models;
using SADM.Models.Requests;
using SADM.Models.Responses;
using SADM.Services;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace SADM.ViewModels
{
    public class GenerateReportViewModel : ViewModelBase
    {
        protected readonly List<string> ReportTypes = new List<string>{
            AppResources.FugueInMyAddress,
            AppResources.FugueInAnotherAddress 
        };
        protected string typeSelected;
        protected IList<Balance> balanceList;
        protected IList<string> nisList;
        protected string nisSelected;
        protected bool fugueInAnotherAddress;
        protected bool textAddressInput;

        protected string street;
        protected string number;
        protected string colony;
        protected string postalCode;
        protected string references;
        protected string comments;
        protected int spartanUserId;
        protected ReportType type;

        public string TypeSelected { get => typeSelected; set => SetProperty(ref typeSelected, value); }
        public string NisSelected { get => nisSelected; set => SetProperty(ref nisSelected, value); }
        public bool FugueInAnotherAddress { get => fugueInAnotherAddress; set => SetProperty(ref fugueInAnotherAddress, value); }
        public bool TextAddressInput { get => textAddressInput; set => SetProperty(ref textAddressInput, value); }
        public ObservableCollection<Pin> PinList { get; set; }

        public string Street { get => street; set => SetProperty(ref street, value); }
        public string Number { get => number; set => SetProperty(ref number, value); }
        public string Colony { get => colony; set => SetProperty(ref colony, value); }
        public string PostalCode { get => postalCode; set => SetProperty(ref postalCode, value); }
        public string References { get => references; set => SetProperty(ref references, value); }
        public string Comments { get => comments; set => SetProperty(ref comments, value); }

        public IAsyncCommand ShowTypeListCommand { get; private set; }
        public IAsyncCommand ShowNisListCommand { get; private set; }
        public ICommand MapClickedCommand { get; private set; }
        public ICommand ChangeAddressInputCommand { get; private set; }
        public IAsyncCommand SendCommand { get; private set; }
        public int SpartanUserId { get => spartanUserId; set => SetProperty(ref spartanUserId, value); }
        public GenerateReportViewModel(INavigationService navigationService, 
                                       ISettingsService settingsService, 
                                       IHudService hudService, 
                                       ISadmApiService apiService) : 
        base(navigationService, settingsService, hudService, apiService)
        {
            //TODO: get NIS List of storage
            nisList = new List<string>();

            //set first item as default selection
            type = ReportType.FugueInMyAddress;
            TypeSelected = ReportTypes.FirstOrDefault();
            NisSelected = nisList.FirstOrDefault();

            //init commands
            ShowTypeListCommand = new AsyncCommand(async () => await ShowListAsync(AppResources.ReportTypeListTitle));
            ShowNisListCommand = new AsyncCommand(async () => await ShowListAsync(AppResources.NisListTitle));
            SendCommand = new AsyncCommand(SendAttemptAsync);
            MapClickedCommand = new Command<MapClickedEventArgs>(MapClicked);
            ChangeAddressInputCommand = new Command(SwitchAddressInput);
            Comments = string.Empty;
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.ContainsKey(string.Empty))
            {
                balanceList = parameters.GetValue<IList<Balance>>(string.Empty);
                nisList = new List<string>(balanceList.Select(balance => balance.Nis));
                NisSelected = nisList.FirstOrDefault();
            }
        }

        public void SwitchAddressInput()
        {
            TextAddressInput = !TextAddressInput;
            type = TextAddressInput ? ReportType.FugueInAnotherAddress : ReportType.FugueInAnotherLocation;
        }

        protected void MapClicked(MapClickedEventArgs args)
        {
            PinList?.Clear();
            PinList?.Add(new Pin{
                Label = "Aquí esta la fuga!",
                Position = args.Point
            });
        }

        protected async Task ShowListAsync(string title)
        {
            if(await HudService.ShowListAsync(title, title == AppResources.ReportTypeListTitle ? ReportTypes : nisList) 
               is string selected)
            {
                if(title == AppResources.ReportTypeListTitle)
                {
                    TypeSelected = selected;
                    type = (ReportType)ReportTypes.IndexOf(selected);
                    if(type == ReportType.FugueInAnotherAddress)
                    {
                        type = TextAddressInput ? ReportType.FugueInAnotherAddress : ReportType.FugueInAnotherLocation;
                    }
                    FugueInAnotherAddress = TypeSelected == AppResources.FugueInAnotherAddress;
                }
                else
                {
                    NisSelected = selected;
                }
            }
        }

        protected async Task SendAttemptAsync()
        {
            DateTime dt = DateTime.Now;
            var balanceSelected = balanceList[nisList.IndexOf(NisSelected)];
            var request = new AddReportRequest
            {
                Type = type,
                Nis = NisSelected,
                Report = new Report
                {
                    Status = ReportStatus.Pending,
                    Email = SettingsService.User.Email,
                    Street = type == ReportType.FugueInMyAddress ? balanceSelected.StreetName : Street,
                    Number = type == ReportType.FugueInMyAddress ? balanceSelected.DoorNumber : Number,
                    Colony = type == ReportType.FugueInMyAddress ? balanceSelected.ColonyName : Colony,
                    PostalCode = PostalCode,
                    References = References,
                    Comments = Comments,
                    Latitude = PinList?.FirstOrDefault()?.Position.Latitude,
                    Longitude = PinList?.FirstOrDefault()?.Position.Longitude,
                    RegisterDate = dt.ToString("dd/MM/yyyy"),
                    RegisterTime = dt.ToString("HH:mm"),
                    User = DatosPago.SpartanUserId
                }
            };

            if(await CallServiceAsync<AddReportRequest, AddReportResponse>(request, AppResources.AddReportLoading, true) 
               is AddReportResponse response && response.Success)
            {
                await HudService.ShowSuccessMessageAsync(string.Format(AppResources.AddReportSuccess, response.ReportId));
                await NavigationService.GoBackAsync();
            }
        }
    }
}
