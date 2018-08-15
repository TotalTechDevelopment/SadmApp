using System;
using SADM.Services;
using SADM.ViewModels;
using Xamarin.Forms;

namespace SADM.Helpers
{
    /// <summary>
    /// Busy.
    /// </summary>
    public class Busy : IDisposable
    {
        protected readonly object sync = new object();
        protected ViewModelBase viewModel;
        protected IHudService hudService;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SADM.Helpers.Busy"/> class.
        /// </summary>
        /// <param name="viewModel">View model.</param>
        /// <param name="processDescription">Process description.</param>
        /// <param name="hudService">Hud service.</param>
        public Busy(ViewModelBase viewModel, string processDescription, IHudService hudService)
        {
            this.viewModel = viewModel;
            this.hudService = hudService;

            this.hudService.ShowProgressAsync(processDescription);
            Device.BeginInvokeOnMainThread(() =>
            {
                lock (sync)
                {
                    this.viewModel.IsBusy = true;
                }
            });
        }

        /// <summary>
        /// Releases all resource used by the <see cref="T:SADM.Helpers.Busy"/> object.
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="T:SADM.Helpers.Busy"/>. The
        /// <see cref="Dispose"/> method leaves the <see cref="T:SADM.Helpers.Busy"/> in an unusable state. After
        /// calling <see cref="Dispose"/>, you must release all references to the <see cref="T:SADM.Helpers.Busy"/> so
        /// the garbage collector can reclaim the memory that the <see cref="T:SADM.Helpers.Busy"/> was occupying.</remarks>
        public void Dispose()
        {
            hudService.HideProgressAsync();
            Device.BeginInvokeOnMainThread(() =>
            {
                lock (sync)
                {
                    viewModel.IsBusy = false;
                }
            });
        }
    }
}
