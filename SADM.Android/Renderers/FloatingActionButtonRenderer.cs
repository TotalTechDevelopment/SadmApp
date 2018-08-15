using System;
using System.ComponentModel;
using Android.Content;
using Android.Content.Res;
using SADM.Controls;
using SADM.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Fab = Android.Support.Design.Widget.FloatingActionButton;

[assembly: ExportRenderer(typeof(FloatingActionButton), typeof(FloatingActionButtonRenderer))]
namespace SADM.Droid.Renderers
{
    public class FloatingActionButtonRenderer : Xamarin.Forms.Platform.Android.AppCompat.ViewRenderer<FloatingActionButton, Fab>
    {
        public FloatingActionButtonRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<FloatingActionButton> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
                return;

            var fab = new Fab(Context)
            {
                BackgroundTintList = ColorStateList.ValueOf(Element.ButtonColor.ToAndroid())
            };

            // set the icon
            if (Element.Image?.File is string imageFile)
            {
                fab.SetImageDrawable(Context.GetDrawable(imageFile));
            }
            fab.Click += Fab_Click;
            SetNativeControl(fab);

        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            Control.BringToFront();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Element.ButtonColor))
            {
                Control.BackgroundTintList = ColorStateList.ValueOf(Element.ButtonColor.ToAndroid());
            }

            if (e.PropertyName == nameof(Element.Image) && Element.Image?.File is string imageFile)
            {
                Control.SetImageDrawable(Context.GetDrawable(imageFile));
            }

            base.OnElementPropertyChanged(sender, e);
        }

        private void Fab_Click(object sender, EventArgs e)
        {
            // proxy the click to the element
            ((IButtonController)Element).SendClicked();
        }
    }
}
