using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Navigation;
using SADM.Extensions;
using SADM.Helpers;
using SADM.Models;
using SADM.Models.Requests;
using SADM.Models.Responses;
using SADM.Services;

namespace SADM.ViewModels
{
    public class MyDataViewModel : ViewModelBase
    {
        protected string firstName;
        protected string lastName;
        protected string secondLastName;
        protected string email;
        protected string street;
        protected string number;
        protected string colony;
        protected string city;
        protected string state;
        protected string postalCode;
        protected string phone;
        protected string password;
        protected string confirmPassword;
        protected string question;
        protected string answer;
        public string FirstName { get => firstName; set => SetProperty(ref firstName, value); }
        public string LastName { get => lastName; set => SetProperty(ref lastName, value); }
        public string SecondLastName { get => secondLastName; set => SetProperty(ref secondLastName, value); }
        public string Email { get => email; set => SetProperty(ref email, value); }
        public string Street { get => street; set => SetProperty(ref street, value); }
        public string Number { get => number; set => SetProperty(ref number, value); }
        public string Colony { get => colony; set => SetProperty(ref colony, value); }
        public string City { get => city; set => SetProperty(ref city, value); }
        public string State { get => state; set => SetProperty(ref state, value); }
        public string PostalCode { get => postalCode; set => SetProperty(ref postalCode, value); }
        public string Phone { get => phone; set => SetProperty(ref phone, value); }
        public string Password { get => password; set => SetProperty(ref password, value); }
        public string ConfirmPassword { get => confirmPassword; set => SetProperty(ref confirmPassword, value); }
        public string Question { get => question; set => SetProperty(ref question, value); }
        public string Answer { get => answer; set => SetProperty(ref answer, value); }

        public IAsyncCommand UpdateDataAttemptCommand => new AsyncCommand(UpdateDataAttemptAsync);

        public IAsyncCommand PhoneHelpCommand => new AsyncCommand(() => HudService.ShowInformationAsync(AppResources.PhoneHelp));
        public IAsyncCommand PasswordHelpCommand => new AsyncCommand(() => HudService.ShowInformationAsync(AppResources.PasswordHelp));
        public IAsyncCommand ConfirmPasswordHelpCommand => new AsyncCommand(() => HudService.ShowInformationAsync(AppResources.PasswordConfirmHelp));

        public MyDataViewModel(INavigationService navigationService, 
                               ISettingsService settingsService, 
                               IHudService hudService, 
                               ISadmApiService apiService) : 
        base(navigationService, settingsService, hudService, apiService)
        {
            FirstName = SettingsService.User.Name;
            LastName = SettingsService.User.LastName;
            SecondLastName = SettingsService.User.SecondLastName;
            Email = SettingsService.User.Email;
            Street = SettingsService.User.Street;
            Number = SettingsService.User.Number;
            Colony = SettingsService.User.Colony;
            City = SettingsService.User.City;
            State = SettingsService.User.State;
            PostalCode = SettingsService.User.PostalCode;
            Phone = SettingsService.User.PhoneNumber;
            Password = SettingsService.User.Password;
            ConfirmPassword = SettingsService.User.Password;
            Question = SettingsService.User.Question;
            Answer = SettingsService.User.Answer;
        }

        protected async Task UpdateDataAttemptAsync()
        {
            if (await UserInputsAreValids())
            {
                var request = new UpdateMyDataRequest
                {
                    Name = FirstName ?? string.Empty,
                    LastName = LastName ?? string.Empty,
                    SecondLastName = SecondLastName ?? string.Empty,
                    Email = Email ?? string.Empty,
                    Street = Street ?? string.Empty,
                    Number = Number ?? string.Empty,
                    Colony = Colony ?? string.Empty,
                    City = City ?? string.Empty,
                    State = State ?? string.Empty,
                    PostalCode = PostalCode ?? string.Empty,
                    PhoneNumber = Phone ?? string.Empty,
                    Password = Password ?? string.Empty,
                    Question = Question ?? string.Empty,
                    Answer = Answer ?? string.Empty
                };

                if (await CallServiceAsync<UpdateMyDataRequest, UpdateMyDataResponse>(request, AppResources.UpdateDataLoading, true) is UpdateMyDataResponse response && response.Success)
                {
                    var newUser = new User
                    {
                        Name = request.Name,
                        LastName = request.LastName,
                        SecondLastName = request.SecondLastName,
                        Email = request.Email,
                        IsActive = request.Active,
                        Street = request.Street,
                        Number = request.Number,
                        Colony = request.Colony,
                        City = request.City,
                        State = request.State,
                        PostalCode = request.PostalCode,
                        PhoneNumber = request.PhoneNumber,
                        Question = request.Question,
                        Answer = request.Answer
                    };
                    await SettingsService.WriteSessionDataAsync(newUser, newUser.Email, true);
                    await HudService.ShowSuccessMessageAsync(AppResources.UpdateDataSuccess);
                }
                else
                {
                    FirstName = SettingsService.User.Name;
                    LastName = SettingsService.User.LastName;
                    SecondLastName = SettingsService.User.SecondLastName;
                    Email = SettingsService.User.Email;
                    Street = SettingsService.User.Street;
                    Number = SettingsService.User.Number;
                    Colony = SettingsService.User.Colony;
                    City = SettingsService.User.City;
                    State = SettingsService.User.State;
                    PostalCode = SettingsService.User.PostalCode;
                    Phone = SettingsService.User.PhoneNumber;
                    Password = SettingsService.User.Password;
                    ConfirmPassword = SettingsService.User.Password;
                    Question = SettingsService.User.Question;
                    Answer = SettingsService.User.Answer;
                }
            }
        }

        protected async Task<bool> UserInputsAreValids()
        {
            var errorList = new List<string>();


            if (string.IsNullOrWhiteSpace(FirstName))
            {
                errorList.Add(AppResources.NameEmpty);
            }
            else if (!FirstName.IsName())
            {
                errorList.Add(AppResources.NameInvalid);
            }

            if (string.IsNullOrWhiteSpace(LastName))
            {
                errorList.Add(AppResources.FirstNameEmpty);
            }
            else if (!LastName.IsName())
            {
                errorList.Add(AppResources.FirstNameInvalid);
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                errorList.Add(AppResources.EmailEmpty);
            }
            else if (!Email.IsEmail())
            {
                errorList.Add(AppResources.EmailInvalid);
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                errorList.Add(AppResources.PasswordEmpty);
            }
            else if (!Password.IsPassword())
            {
                errorList.Add(AppResources.PasswordInvalid);
            }

            if (string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                errorList.Add(AppResources.ConfirmPasswordRequired);
            }
            else if (ConfirmPassword != Password)
            {
                errorList.Add(AppResources.ConfirmPasswordError);
            }

            if (string.IsNullOrWhiteSpace(Question))
            {
                errorList.Add(AppResources.QuestionRequired);
            }
            if (string.IsNullOrWhiteSpace(Answer))
            {
                errorList.Add(AppResources.AnswerRequired);
            }

            if (!string.IsNullOrEmpty(Phone) && !Phone.IsPhone())//validate phone
            {
                errorList.Add(AppResources.PhoneNumberInvalid);
            }
            if (!string.IsNullOrEmpty(PostalCode) && !PostalCode.IsZipCode())//validate postal code
            {
                errorList.Add(AppResources.ZipCodeInvalid);
            }

            if(!errorList.Any() &&
               (FirstName ?? string.Empty) == (SettingsService.User.Name ?? string.Empty) &&
               (LastName ?? string.Empty) == (SettingsService.User.LastName ?? string.Empty) &&
               (SecondLastName ?? string.Empty) == (SettingsService.User.SecondLastName ?? string.Empty) &&
               (Email ?? string.Empty) == (SettingsService.User.Email ?? string.Empty) &&
               (Street ?? string.Empty) == (SettingsService.User.Street ?? string.Empty) &&
               (Number ?? string.Empty) == (SettingsService.User.Number ?? string.Empty) &&
               (Colony ?? string.Empty) == (SettingsService.User.Colony ?? string.Empty) &&
               (City ?? string.Empty) == (SettingsService.User.City ?? string.Empty) &&
               (State ?? string.Empty) == (SettingsService.User.State ?? string.Empty) &&
               (PostalCode ?? string.Empty) == (SettingsService.User.PostalCode ?? string.Empty) &&
               (Phone ?? string.Empty) == (SettingsService.User.PhoneNumber ?? string.Empty) &&
               (Password ?? string.Empty) == (SettingsService.User.Password ?? string.Empty) &&
               (ConfirmPassword ?? string.Empty) == (SettingsService.User.Password ?? string.Empty) &&
               (Question ?? string.Empty) == (SettingsService.User.Question ?? string.Empty) &&
               (Answer ?? string.Empty) == (SettingsService.User.Answer ?? string.Empty))
            {
                errorList.Add(AppResources.UpdateDataUnchanged);
            }


            if (errorList.Any())
            {
                await HudService.ShowErrorListAsync(errorList);
            }
            return !errorList.Any();
        }
    }
}
