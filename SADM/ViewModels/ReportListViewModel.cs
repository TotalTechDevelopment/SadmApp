using System.Collections.Generic;
using Prism.Navigation;
using SADM.Controls;
using SADM.Models;
using SADM.Services;

namespace SADM.ViewModels
{
    public class ReportListViewModel : ViewModelBase
    {
        public ObservableCollectionExt<Report> ReportList { get; private set; }

        public ReportListViewModel(INavigationService navigationService, 
                                   ISettingsService settingsService, 
                                   IHudService hudService, 
                                   ISadmApiService apiService) : 
        base(navigationService, settingsService, hudService, apiService)
        {
            ReportList = new ObservableCollectionExt<Report>();
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.ContainsKey(string.Empty) && parameters.GetValue<IList<Report>>(string.Empty) is IList<Report> reportList)
            {
                ReportList.Reset(reportList);
            }
        }
    }
}