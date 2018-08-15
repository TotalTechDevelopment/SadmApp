using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace SADM.Controls
{
    public partial class ChartBar : Grid
    {
        protected IList<int> Source;
        protected bool isLoaded;

        public int Test
        {
            get => (int)GetValue(TestProperty);
            set => SetValue(TestProperty, value);
        }

        public static readonly BindableProperty TestProperty =
            BindableProperty.Create(nameof(Test), typeof(int), typeof(ChartBar), default(int));

        public ChartBar()
        {
            Source = new List<int>{50,70,40,30,50,45,55,65,75,85,95,100};
        }
    }
}
