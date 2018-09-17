using System.Collections.Generic;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using SADM.Controls;
using SADM.Enums;
using Xamarin.Forms;

namespace SADM.Services
{
    public class HudService : IHudService
    {
        protected ProgressDialog progressDialog;

        /// <summary>
        /// Shows the error async.
        /// </summary>
        /// <returns>The error async.</returns>
        /// <param name="error">Error.</param>
        public Task ShowErrorAsync(string error)
        {
            return ShowErrorAsync(error, AppResources.HudContineDefault);
        }

        /// <summary>
        /// Shows the error async.
        /// </summary>
        /// <returns>The error async.</returns>
        /// <param name="error">Error.</param>
        /// <param name="acceptButtonText">Accept button text.</param>
        public Task ShowErrorAsync(string error, string acceptButtonText)
        {
            return ShowErrorListAsync(new List<string> { error }, acceptButtonText);
        }

        /// <summary>
        /// Shows the error list async.
        /// </summary>
        /// <returns>The error list async.</returns>
        /// <param name="errorList">Error list.</param>
        public Task ShowErrorListAsync(IList<string> errorList)
        {
            return ShowErrorListAsync(errorList, AppResources.HudContineDefault);
        }

        /// <summary>
        /// Shows the error list async.
        /// </summary>
        /// <returns>The error list async.</returns>
        /// <param name="errorList">Error list.</param>
        /// <param name="acceptButtonText">Accept button text.</param>
        public async Task ShowErrorListAsync(IList<string> errorList, string acceptButtonText)
        {
            var tcs = new TaskCompletionSource<bool>();
            var alert = new AlertPopup(tcs, NotificationType.Error, ToErrorString(errorList), acceptButtonText);
            await PopupNavigation.PushAsync(alert);
            await tcs.Task;
            await PopupNavigation.RemovePageAsync(alert);
        }

        /// <summary>
        /// Shows the progress async.
        /// </summary>
        /// <returns>The progress async.</returns>
        /// <param name="message">Message.</param>
        public async Task ShowProgressAsync(string message)
        {
            if(message != null)
            {
                await HideProgressAsync(false);
                progressDialog = new ProgressDialog(message);
                await PopupNavigation.PushAsync(progressDialog);
            }
        }

        public void ShowProgress(string message)
        {
            if (message != null)
            {
                HideProgress();
                progressDialog = new ProgressDialog(message);
                PopupNavigation.PushAsync(progressDialog);
            }
        }

        /// <summary>
        /// Hides the progress async.
        /// </summary>
        /// <returns>The progress async.</returns>
        public async Task HideProgressAsync(bool animate = false)
        {
            if (progressDialog != null)
            {
                await Task.Delay(1000);
                await PopupNavigation.RemovePageAsync(progressDialog, animate);
            }
        }

        public void HideProgress()
        {
            if (progressDialog != null)
            {
                PopupNavigation.RemovePageAsync(progressDialog);
                progressDialog = null;
            }
        }


        /// <summary>
        /// Tos the error string.
        /// </summary>
        /// <returns>The error string.</returns>
        /// <param name="errorList">Error list.</param>
        protected string ToErrorString(IList<string> errorList)
        {
            var errorString = string.Empty;
            foreach (var error in errorList)
            {
                errorString += string.IsNullOrEmpty(errorString) ? string.Empty : System.Environment.NewLine;
                errorString += string.Format(AppResources.ErrorItem, error);

            }
            return errorString;
        }

        public async Task<bool> ShowQuestionAsync(string title, string message, string acceptButtonText, string cancelButtonText)
        {
            var tcs = new TaskCompletionSource<bool>();
            var alert = new QuestionPopup(tcs, title, message, acceptButtonText, cancelButtonText);
            await PopupNavigation.PushAsync(alert);
            var result = await tcs.Task;
            await PopupNavigation.RemovePageAsync(alert);
            return result;
        }

        public async Task<string> ShowRecoverPasswordAsync(string title, string message, string acceptButtonText, string cancelButtonText)
        {
            var tcs = new TaskCompletionSource<bool>();
            RecoverPasswordPopup alert = new RecoverPasswordPopup(tcs, title, message, acceptButtonText, cancelButtonText);
            await PopupNavigation.PushAsync(alert);
            var result = await tcs.Task;
            await PopupNavigation.RemovePageAsync(alert);
            if(result){
                var emailText =  alert.FindByName<Entry>("EmailText");
                return  emailText.Text;
            }
            else{
                return "";
            }

        }

        public async Task ShowInformationAsync(string message)
        {
            await ShowInformationAsync(message, "OK");
        }

        public async Task ShowInformationAsync(string message, string acceptButtonText){
            var tcs = new TaskCompletionSource<bool>();
            var alert = new AlertPopup(tcs, NotificationType.Information, message, acceptButtonText);
            await PopupNavigation.PushAsync(alert);
            await tcs.Task;
            await PopupNavigation.RemovePageAsync(alert);
        }

        public async Task ShowSuccessMessageAsync(string message)
        {
            await ShowSuccessMessageAsync(message, "OK");
        }
        public async Task ShowSuccessMessageAsync(string message, string acceptButtonText)
        {
            var tcs = new TaskCompletionSource<bool>();
            var alert = new AlertPopup(tcs, NotificationType.Success, message, acceptButtonText);
            await PopupNavigation.PushAsync(alert);
            await tcs.Task;
            await PopupNavigation.RemovePageAsync(alert);
        }

        public async Task ShowExampleAsync(string message, string exampleImage)
        {
            await ShowExampleAsync(message, exampleImage, "OK");
        }

        public async Task ShowExampleAsync(string message, string exampleImage, string acceptButtonText)
        {
            var tcs = new TaskCompletionSource<bool>();
            var alert = new ExamplePopup(tcs, message, exampleImage, acceptButtonText);
            await PopupNavigation.PushAsync(alert);
            await tcs.Task;
            await PopupNavigation.RemovePageAsync(alert);
        }

        public async Task<DownloadBillPopupResult> ShowDownloadBillAsync(string nis, string first, string second, string third, string acceptButtonText)
        {
            var tcs = new TaskCompletionSource<DownloadBillPopupResult>();
            var alert = new DownloadBillPopup(tcs, nis, first, second, third, acceptButtonText);
            await PopupNavigation.PushAsync(alert);
            var result = await tcs.Task;
            await PopupNavigation.RemovePageAsync(alert);
            return result;
        }

        public async Task<string> ShowListAsync(string title, IEnumerable<string> list)
        {
            return await ShowListAsync(title, list, AppResources.CancelButton);
        }

        public async Task<string> ShowListAsync(string title, IEnumerable<string> list, string cancelButtonText)
        {
            var tcs = new TaskCompletionSource<string>();
            var alert = new ListPopup(tcs, title, list, cancelButtonText);
            await PopupNavigation.PushAsync(alert);
            var result = await tcs.Task;
            await PopupNavigation.RemovePageAsync(alert);
            return result;
        }

        /*public async Task ShowTermsAndConditionsAsync(string title, string termsAndConditions, string continueButtonText)
        {
            var tcs = new TaskCompletionSource<bool>();
            var alert = new TermsAndConditionsPopUp(tcs, title, termsAndConditions, continueButtonText);
            await PopupNavigation.PushAsync(alert);
            await tcs.Task;
            await PopupNavigation.RemovePageAsync(alert);
        }*/

        public async Task ShowTermsAndConditionsAsync(string title, string termsAndConditions, string title2, string privacity, string continueButtonText)
        {
            var tcs = new TaskCompletionSource<bool>();
            var alert = new TermsAndConditionsPopUp(tcs, title, termsAndConditions, title2, privacity, continueButtonText);
            await PopupNavigation.PushAsync(alert);
            await tcs.Task;
            await PopupNavigation.RemovePageAsync(alert);
        }
    }
}