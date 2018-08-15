﻿using System.Collections.ObjectModel;
using Prism.Navigation;
using SADM.Helpers;
using SADM.Models;
using SADM.Services;

namespace SADM.ViewModels
{
    public class NotificationsViewModel : ViewModelBase
    {
        public ObservableCollection<Notification> NotificationList { get; private set; }
        public IAsyncCommand ShowNotificationCommand { get; private set; }

        public NotificationsViewModel(INavigationService navigationService, 
                                      ISettingsService settingsService, 
                                      IHudService hudService, 
                                      ISadmApiService apiService) : 
        base(navigationService, settingsService, hudService, apiService)
        {
            ShowNotificationCommand = new AsyncCommand(GoToPageAsync<Views.NotificationPage>);
            NotificationList = new ObservableCollection<Notification> { 
                new Notification {
                    Title = "Tu pago se ha realizado", 
                    Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do euismod", 
                    Type = Enums.NotificationType.Success
                },
                new Notification {
                    Title = "Se ha reportado una fuga en el...",
                    Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do euismod",
                    Type = Enums.NotificationType.Error
                },
                new Notification {
                    Title = "Tu reporte se ha generado",
                    Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do euismod",
                    Type = Enums.NotificationType.Success
                },
                new Notification {
                    Title = "La fecha de vencimiento de tu...",
                    Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do euismod",
                    Type = Enums.NotificationType.Success
                },
                new Notification {
                    Title = "La fecha de vencimiento de tu...",
                    Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do euismod",
                    Type = Enums.NotificationType.Error
                },
                new Notification {
                    Title = "La fecha de vencimiento de tu...",
                    Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do euismod",
                    Type = Enums.NotificationType.Success
                }
            };
        }
    }
}
