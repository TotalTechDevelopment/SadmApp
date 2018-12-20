using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Navigation;
using SADM.Extensions;
using SADM.Helpers;
using SADM.Models;
using SADM.Models.Requests;
using SADM.Services;

namespace SADM.ViewModels
{
    /// <summary>
    /// Sign up ViewModel.
    /// </summary>
    public class SignUpViewModel : ViewModelBase
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
        protected bool acceptTermsAndConditions;

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
        public bool AcceptTermsAndConditions { get => acceptTermsAndConditions; set => SetProperty(ref acceptTermsAndConditions, value); }

        public IAsyncCommand SignUpAttemptCommand => new AsyncCommand(SignUpAttemptAsync);

        public IAsyncCommand PhoneHelpCommand => new AsyncCommand(() => HudService.ShowInformationAsync(AppResources.PhoneHelp));
        public IAsyncCommand PasswordHelpCommand => new AsyncCommand(() => HudService.ShowInformationAsync(AppResources.PasswordHelp));
        public IAsyncCommand ConfirmPasswordHelpCommand => new AsyncCommand(() => HudService.ShowInformationAsync(AppResources.PasswordConfirmHelp));
        public IAsyncCommand TermsAndConditionsCommand => new AsyncCommand(() => HudService.ShowTermsAndConditionsAsync(AppResources.TermsAndConditionsTitle, 
                                                                                                                        AppResources.TermsAndConditionsText, 
                                                                                                                        AppResources.PrivacityTitle,
                                                                                                                        AppResources.PrivacityText,
                                                                                                                        AppResources.CloseButton));

        public SignUpViewModel(INavigationService navigationService, 
                               ISettingsService settingsService, 
                               IHudService hudService, 
                               ISadmApiService apiService) : 
        base(navigationService, settingsService, hudService, apiService)
        {
        }

        protected async Task SignUpAttemptAsync()
        {
            if (await UserInputsForSignUpAreValids())
            {
                var signUpRequest = new SignUpRequest
                {
                    Name = FirstName,
                    LastName = LastName,
                    SecondLastName = SecondLastName,
                    Email = Email,
                    Street = Street,
                    Number = Number, 
                    Colony = colony,
                    City = City,
                    State = State,
                    PostalCode = PostalCode,
                    PhoneNumber = long.Parse(Phone),
                    Password = Password,
                    Question = Question,
                    Answer = Answer
                };
                await GoToPageAsync<Views.AssociateContractPage>(signUpRequest);
            }
        }

        /// <summary>
        /// Validate users inputs for sign up are and show errors.
        /// </summary>
        /// <returns><c>true</c>, if user inputs for sign up are valids, <c>false</c> otherwise.</returns>
        protected async Task<bool> UserInputsForSignUpAreValids()
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
            if (!AcceptTermsAndConditions) 
            { 
                errorList.Add(AppResources.TermsAndConditionsError); 
            }
            if (errorList.Any())
            {
                await HudService.ShowErrorListAsync(errorList);
            }
            return !errorList.Any();
        }
    }
}
