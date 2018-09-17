using System.Collections.Generic;
using System.Threading.Tasks;
using SADM.Enums;

namespace SADM.Services
{
    public interface IHudService
    {
        /// <summary>
        /// Shows the error async.
        /// </summary>
        /// <returns>The error async.</returns>
        /// <param name="error">Error.</param>
        Task ShowErrorAsync(string error);

        /// <summary>
        /// Shows the error async.
        /// </summary>
        /// <returns>The error async.</returns>
        /// <param name="error">Error.</param>
        /// <param name="acceptButtonText">Accept button text.</param>
        Task ShowErrorAsync(string error, string acceptButtonText);

        /// <summary>
        /// Shows the error list async.
        /// </summary>
        /// <returns>The error list async.</returns>
        /// <param name="errorList">Error list.</param>
        Task ShowErrorListAsync(IList<string> errorList);

        /// <summary>
        /// Shows the error list async.
        /// </summary>
        /// <returns>The error list async.</returns>
        /// <param name="errorList">Error list.</param>
        /// <param name="acceptButtonText">Accept button text.</param>
        Task ShowErrorListAsync(IList<string> errorList, string acceptButtonText);

        /// <summary>
        /// Shows the progress async.
        /// </summary>
        /// <returns>The progress async.</returns>
        /// <param name="message">Message.</param>
        Task ShowProgressAsync(string message);
        void ShowProgress(string message);

        /// <summary>
        /// Hides the progress async.
        /// </summary>
        /// <returns>The progress async.</returns>
        Task HideProgressAsync(bool animate = true);
        void HideProgress();

        Task<bool> ShowQuestionAsync(string title, string message, string acceptButtonText, string cancelButtonText);
        Task<string> ShowRecoverPasswordAsync(string title, string message, string acceptButtonText, string cancelButtonText);
        Task ShowInformationAsync(string message);
        Task ShowInformationAsync(string message, string acceptButtonText);
        Task ShowSuccessMessageAsync(string message);
        Task ShowSuccessMessageAsync(string message, string acceptButtonText);
        Task ShowExampleAsync(string message, string exampleImage);
        Task ShowExampleAsync(string message, string exampleImage, string acceptButtonText);
        Task<DownloadBillPopupResult> ShowDownloadBillAsync(string nis, string first, string second, string third, string acceptButtonText);

        Task<string> ShowListAsync(string title, IEnumerable<string> list);
        Task<string> ShowListAsync(string title, IEnumerable<string> list, string cancelButtonText);

        //Task ShowTermsAndConditionsAsync(string title, string termsAndConditions, string continueButton);
        Task ShowTermsAndConditionsAsync(string title, string termsAndConditions, string title2, string privacity, string continueButtonText);
    }
}