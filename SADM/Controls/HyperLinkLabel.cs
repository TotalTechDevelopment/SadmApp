using System.Windows.Input;
using SADM.Effects;
using Xamarin.Forms;

namespace SADM.Controls
{
    /// <summary>
    /// Hyper link label.
    /// </summary>
    public class HyperLinkLabel : Label
    {
        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>The command.</value>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// The command property.
        /// </summary>
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(InputBox), null);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SADM.Controls.HyperLinkLabel"/> class.
        /// </summary>
        public HyperLinkLabel()
        {
            TextColor = Color.Blue;
            Effects.Add(new UnderlinedLabelEffect());
            GestureRecognizers.Add(new TapGestureRecognizer 
            { 
                Command = new Command(() => Command?.Execute(null)) 
            });
        }
    }
}
