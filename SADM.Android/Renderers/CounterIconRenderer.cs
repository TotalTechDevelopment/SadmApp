using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics;
using Android.Util;
using SADM.Controls;
using SADM.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CounterIcon), typeof(CounterIconRenderer))]
namespace SADM.Droid.Renderers
{
    public class CounterIconRenderer : ImageRenderer
    {
        public CounterIconRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == CounterIcon.CounterProperty.PropertyName)
            {
                Invalidate();
            }
        }

        protected override bool DrawChild(Canvas canvas, Android.Views.View child, long drawingTime)
        {
            if(Element is CounterIcon counterIcon && counterIcon.Counter != 0)
            {
                try
                {
                    //Add Focus
                    var paint = new Paint
                    {
                        Color = Android.Graphics.Color.Gray
                    };
                    paint.SetStyle(Paint.Style.Fill);
                    canvas.DrawCircle(canvas.Width / 2, canvas.Height / 2, canvas.Width / 2, paint);
                    canvas.Save();

                    //Add image
                    var result = base.DrawChild(canvas, child, drawingTime);
                    //canvas.Restore();

                    //Add Circle
                    paint = new Paint
                    {
                        Color = Android.Graphics.Color.Red
                    };
                    paint.SetStyle(Paint.Style.Fill);
                    canvas.DrawCircle(canvas.Width * 0.75f, canvas.Height / 4, canvas.Width / 4, paint);
                    canvas.Save();
                    
                    //Add number
                    var number = counterIcon.Counter < 10 ? $" {counterIcon.Counter} " : $"{counterIcon.Counter}";
                    var textSize = 8;
                    paint = new Paint(PaintFlags.AntiAlias)
                    {
                        TextAlign = Paint.Align.Center,
                        TextSize = PixelsFromDp(textSize),
                        Color = Android.Graphics.Color.White
                    };

                    var space = (paint.Descent() + paint.Ascent()) / 2;
                    var horizontalSpace = paint.MeasureText(number, 0, 2) / 2;
                    canvas.DrawText(number, canvas.Width * 0.75f, canvas.Height / 4 - space, paint);
                    canvas.Save();

                    paint.Dispose();
                    return result;
                }
                catch
                {
                    return base.DrawChild(canvas, child, drawingTime);
                }
            }
            return base.DrawChild(canvas, child, drawingTime);
        }

        public int PixelsFromDp(int dp)
        {
            return (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, dp, Context.Resources.DisplayMetrics);
        }
    }
}
