using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using CoreGraphics;
using CoreText;
using Foundation;
using SADM.Controls;
using SADM.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CounterIcon), typeof(CounterIconRenderer))]
namespace SADM.iOS.Renderers
{
    public class CounterIconRenderer : ImageRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == CounterIcon.CounterProperty.PropertyName)
            {
                DrawCounter();
            }
        }

        protected void DrawCounter()
        {
            
        }

        public override void Draw(CGRect rect)
        {
            if (Element is CounterIcon counterIcon && counterIcon.Counter != 0)
            {
                var context = UIGraphics.GetCurrentContext();
                var fileImageSource = counterIcon.Source as FileImageSource;
                counterIcon.Source = "";

                var bg = new CGPoint(rect.Width / 2, rect.Height / 2);
                var bgPath = new UIBezierPath();
                bgPath.AddArc(bg, rect.Width / 2, 0, 2.0f * (float)Math.PI, true);
                bgPath.ClosePath();
                UIColor.Gray.SetFill();
                bgPath.Fill();

                float width = (float)counterIcon.Width;
                float height = (float)counterIcon.Height;

                var image = UIImage.FromFile(fileImageSource.File);
                int kMaxResolution = 1280; // Or whatever

                CGImage imgRef = image.CGImage;

                CGAffineTransform transform = CGAffineTransform.MakeIdentity();
                RectangleF bounds = new RectangleF(0, 0, width, height);

                if (width > kMaxResolution || height > kMaxResolution)
                {
                    float ratio = width / height;

                    if (ratio > 1)
                    {
                        bounds.Size = new SizeF(kMaxResolution, bounds.Size.Width / ratio);
                    }
                    else
                    {
                        bounds.Size = new SizeF(bounds.Size.Height * ratio, kMaxResolution);
                    }
                }

                float scaleRatio = bounds.Size.Width / width;
                SizeF imageSize = new SizeF((float)counterIcon.Width, (float)counterIcon.Height);//new SizeF(imgRef.Width, imgRef.Height);
                UIImageOrientation orient = image.Orientation;
                float boundHeight;
                switch (orient)
                {
                    case UIImageOrientation.Up:                                        //EXIF = 1
                        transform = CGAffineTransform.MakeIdentity();
                        break;
                    // TODO: Add other Orientations
                    case UIImageOrientation.Right:                                     //EXIF = 8
                        boundHeight = bounds.Size.Height;
                        bounds.Size = new SizeF(boundHeight, bounds.Size.Width);
                        transform = CGAffineTransform.MakeTranslation(imageSize.Height, 0);
                        transform = CGAffineTransform.Rotate(transform, (float)Math.PI / 2.0f);
                        break;
                    default:
                        throw new Exception("Invalid image orientation");

                }

                UIGraphics.BeginImageContext(bounds.Size);
                var tx = default(nfloat);
                var ty = default(nfloat);
                var sx = default(nfloat);
                var sy = default(nfloat);
                if (orient == UIImageOrientation.Right || orient == UIImageOrientation.Left)
                {
                    sx = -scaleRatio;
                    sy = scaleRatio;
                    tx = -height;
                    ty = 0;
                }
                else
                {
                    sx = scaleRatio;
                    sy = -scaleRatio;
                    tx = 0;
                    ty = -height;
                }
                context.ScaleCTM(sx, sy);
                context.TranslateCTM(tx, ty);

                context.ConcatCTM(transform);

                context.DrawImage(new RectangleF(0, 0, width, height), imgRef);

                UIGraphics.PopContext();

                var smallCircle = new CGPoint(rect.Width / 4 * 3, rect.Height / 4 * 3);
                var radius = rect.Width / 4;
                var midPath = new UIBezierPath();
                midPath.MoveTo(smallCircle);
                midPath.AddArc(smallCircle, radius, 0, 2.0f * (float)Math.PI, true);
                midPath.ClosePath();
                UIColor.Red.SetFill();
                midPath.Fill();
            }
            else
            {
                base.Draw(rect);
            }
        }

    }
}
