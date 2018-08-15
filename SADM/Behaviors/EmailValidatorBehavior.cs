using System;
using System.Linq;
using SADM.Controls;
using SADM.Extensions;
using Xamarin.Forms;

namespace SADM.Behaviors
{
    public class EmailValidatorBehavior : Behavior<InputBox>
    {
        public static readonly BindablePropertyKey IsValidPropertyKey = 
            BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(EmailValidatorBehavior), false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            protected set => SetValue(IsValidPropertyKey, value);
        }

        protected override void OnAttachedTo(InputBox bindable)
        {
            bindable.TextChanged += HandleTextChanged;
        }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                if(sender is Entry entry)
                {
                    IsValid = e.NewTextValue.IsEmail();
                    entry.TextColor = IsValid ? Color.Black : Color.Red;
                }
            }
        }

        /// <param name="bindable">To be added.</param>
        /// <summary>
        /// Raises the detaching from event.
        /// </summary>
        protected override void OnDetachingFrom(InputBox bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
        }
    }
}
