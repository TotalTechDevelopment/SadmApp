using SADM.Controls;
using SADM.Extensions;
using Xamarin.Forms;

namespace SADM.Behaviors
{
    public class OnlyDigitsValidatorBehavior : Behavior<InputBox>
    {
        public int Max { get; set; }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.NewTextValue) && sender is Entry entry)
            {
                entry.Text = e.NewTextValue.IsIntegerNumber() && e.NewTextValue.Length <= Max
                        ? e.NewTextValue : e.OldTextValue;
            }
        }

        protected override void OnAttachedTo(InputBox bindable)
        {
            bindable.TextChanged += HandleTextChanged;
        }

        protected override void OnDetachingFrom(InputBox bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
        }
    }
}
