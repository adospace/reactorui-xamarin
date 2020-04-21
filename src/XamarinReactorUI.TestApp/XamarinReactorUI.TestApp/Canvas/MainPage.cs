using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinReactorUI.TestApp.Canvas
{
    public class MainPageState : IState
    { 
        public double RotationAngle { get; set; }
    }

    public class MainPage : RxComponent<MainPageState>
    {
        private SKCanvasView _canvasView;

        public override VisualNode Render()
        {
            return new RxContentPage()
            {
                new RxStackLayout()
                {
                    new RxLabel("Rotation Angle"),
                    new RxSlider()
                        .Minimum(0)
                        .Maximum(360.0)
                        .Value(State.RotationAngle)
                        .OnValueChanged(v => SetState(s => 
                        {
                            s.RotationAngle = v.NewValue;
                            _canvasView?.InvalidateSurface();
                        }))
                        .HFill()
                        .VStart(),
                    new RxSKCanvasView(canvasView => _canvasView = canvasView)
                        .OnPaintSurface(OnPaintCanvasSurface)
                        .VFillAndExpand()
                }
            };
        }

        private void OnPaintCanvasSurface(SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            string text = "OUTLINE";

            // Create an SKPaint object to display the text
            SKPaint textPaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 1,
                FakeBoldText = true,
                Color = SKColors.Blue
            };

            // Adjust TextSize property so text is 95% of screen width
            float textWidth = textPaint.MeasureText(text);
            textPaint.TextSize = 0.95f * info.Width * textPaint.TextSize / textWidth;

            // Find the text bounds
            SKRect textBounds = new SKRect();
            textPaint.MeasureText(text, ref textBounds);

            // Calculate offsets to center the text on the screen
            float xText = info.Width / 2 - textBounds.MidX;
            float yText = info.Height / 2 - textBounds.MidY;

            // And draw the text
            canvas.RotateDegrees((float)State.RotationAngle, info.Width / 2, info.Height / 2);
            canvas.DrawText(text, xText, yText, textPaint);
        }
    }
}
