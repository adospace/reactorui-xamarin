using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using GradientStop = Xamarin.Forms.PancakeView.GradientStop;
using GradientStopCollection = Xamarin.Forms.PancakeView.GradientStopCollection;

namespace XamarinReactorUI
{
    public interface IRxPancakeView
    {
        int Sides { get; set; }
        CornerRadius CornerRadius { get; set; }
        //float BorderThickness { get; set; }
        //DashPattern BorderDashPattern { get; set; }
        //Color BorderColor { get; set; }
        //BorderDrawingStyle BorderDrawingStyle { get; set; }
        //int BackgroundGradientAngle { get; set; }
        GradientStopCollection BackgroundGradientStops { get; set; }
        //int BorderGradientAngle { get; set; }
        //GradientStopCollection BorderGradientStops { get; set; }
        double OffsetAngle { get; set; }
        DropShadow Shadow { get; set; }
    }

    public class RxPancakeView<T> : RxContentView<T>, IRxPancakeView where T : PancakeView, new()
    {
        public RxPancakeView()
        {
        }

        public RxPancakeView(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public int Sides { get; set; } = (int)PancakeView.SidesProperty.DefaultValue;
        public CornerRadius CornerRadius { get; set; } = (CornerRadius)PancakeView.CornerRadiusProperty.DefaultValue;
        //public float BorderThickness { get; set; } = (float)PancakeView.BorderThicknessProperty.DefaultValue;
        //public DashPattern BorderDashPattern { get; set; } = (DashPattern)PancakeView.BorderDashPatternProperty.DefaultValue;
        //public Color BorderColor { get; set; } = (Color)PancakeView.BorderColorProperty.DefaultValue;
        //public BorderDrawingStyle BorderDrawingStyle { get; set; } = (BorderDrawingStyle)PancakeView.BorderDrawingStyleProperty.DefaultValue;
        //public int BackgroundGradientAngle { get; set; } = (int)PancakeView.BackgroundGradientAngleProperty.DefaultValue;
        public GradientStopCollection BackgroundGradientStops { get; set; } = (GradientStopCollection)PancakeView.BackgroundGradientStopsProperty.DefaultValue;
        //public int BorderGradientAngle { get; set; } = (int)PancakeView.BorderGradientAngleProperty.DefaultValue;
        //public GradientStopCollection BorderGradientStops { get; set; } = (GradientStopCollection)PancakeView.BorderGradientStopsProperty.DefaultValue;
        public double OffsetAngle { get; set; } = (double)PancakeView.OffsetAngleProperty.DefaultValue;
        public DropShadow Shadow { get; set; } = (DropShadow)PancakeView.ShadowProperty.DefaultValue;

        protected override void OnUpdate()
        {
            NativeControl.Sides = Sides;
            NativeControl.CornerRadius = CornerRadius;
            //NativeControl.BorderThickness = BorderThickness;
            //NativeControl.BorderDashPattern = BorderDashPattern;
            //NativeControl.BorderColor = BorderColor;
            //NativeControl.BorderDrawingStyle = BorderDrawingStyle;
            //NativeControl.BackgroundGradientAngle = BackgroundGradientAngle;
            NativeControl.BackgroundGradientStops = BackgroundGradientStops;
            //NativeControl.BorderGradientAngle = BorderGradientAngle;
            //NativeControl.BorderGradientStops = BorderGradientStops;
            NativeControl.OffsetAngle = OffsetAngle;
            NativeControl.Shadow = Shadow;

            base.OnUpdate();
        }
    }

    public class RxPancakeView : RxPancakeView<PancakeView>
    {
        public RxPancakeView()
        {
        }

        public RxPancakeView(Action<PancakeView> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public static class RxPancakeViewExtensions
    {
        public static T Sides<T>(this T pancakeview, int sides) where T : IRxPancakeView
        {
            pancakeview.Sides = sides;
            return pancakeview;
        }

        public static T CornerRadius<T>(this T pancakeview, CornerRadius cornerRadius) where T : IRxPancakeView
        {
            pancakeview.CornerRadius = cornerRadius;
            return pancakeview;
        }

        public static T CornerRadius<T>(this T pancakeview, double topLeft, double topRight, double bottomLeft, double bottomRight) where T : IRxPancakeView
        {
            pancakeview.CornerRadius = new CornerRadius(topLeft, topRight, bottomLeft, bottomRight);
            return pancakeview;
        }

        public static T CornerRadius<T>(this T pancakeview, double uniformRadius) where T : IRxPancakeView
        {
            pancakeview.CornerRadius = new CornerRadius(uniformRadius);
            return pancakeview;
        }

        //public static T BorderThickness<T>(this T pancakeview, float borderThickness) where T : IRxPancakeView
        //{
        //    pancakeview.BorderThickness = borderThickness;
        //    return pancakeview;
        //}

        //public static T BorderDashPattern<T>(this T pancakeview, DashPattern borderDashPattern) where T : IRxPancakeView
        //{
        //    pancakeview.BorderDashPattern = borderDashPattern;
        //    return pancakeview;
        //}

        //public static T BorderColor<T>(this T pancakeview, Color borderColor) where T : IRxPancakeView
        //{
        //    pancakeview.BorderColor = borderColor;
        //    return pancakeview;
        //}

        //public static T BorderDrawingStyle<T>(this T pancakeview, BorderDrawingStyle borderDrawingStyle) where T : IRxPancakeView
        //{
        //    pancakeview.BorderDrawingStyle = borderDrawingStyle;
        //    return pancakeview;
        //}

        //public static T BackgroundGradientAngle<T>(this T pancakeview, int backgroundGradientAngle) where T : IRxPancakeView
        //{
        //    pancakeview.BackgroundGradientAngle = backgroundGradientAngle;
        //    return pancakeview;
        //}

        public static T BackgroundGradientStops<T>(this T pancakeview, GradientStopCollection gradientStops) where T : IRxPancakeView
        {
            pancakeview.BackgroundGradientStops = gradientStops;
            return pancakeview;
        }

        public static T BackgroundGradientStops<T>(this T pancakeview, IEnumerable<GradientStop> gradientStops) where T : IRxPancakeView
        {
            var gradientStopsCollection = new GradientStopCollection();
            foreach (var gradientStop in gradientStops)
                gradientStopsCollection.Add(gradientStop);
            pancakeview.BackgroundGradientStops = gradientStopsCollection;
            return pancakeview;
        }

        public static T BackgroundGradientStops<T>(this T pancakeview, params GradientStop[] gradientStops) where T : IRxPancakeView
        {
            var gradientStopsCollection = new GradientStopCollection();
            foreach (var gradientStop in gradientStops)
                gradientStopsCollection.Add(gradientStop);
            pancakeview.BackgroundGradientStops = gradientStopsCollection;
            return pancakeview;
        }

        //public static T BorderGradientAngle<T>(this T pancakeview, int borderGradientAngle) where T : IRxPancakeView
        //{
        //    pancakeview.BorderGradientAngle = borderGradientAngle;
        //    return pancakeview;
        //}

        //public static T BorderGradientStops<T>(this T pancakeview, GradientStopCollection borderGradientStops) where T : IRxPancakeView
        //{
        //    pancakeview.BorderGradientStops = borderGradientStops;
        //    return pancakeview;
        //}

        //public static T BorderGradientStops<T>(this T pancakeview, IEnumerable<GradientStop> gradientStops) where T : IRxPancakeView
        //{
        //    var gradientStopsCollection = new GradientStopCollection();
        //    foreach (var gradientStop in gradientStops)
        //        gradientStopsCollection.Add(gradientStop);
        //    pancakeview.BorderGradientStops = gradientStopsCollection;
        //    return pancakeview;
        //}

        //public static T BorderGradientStops<T>(this T pancakeview, params GradientStop[] gradientStops) where T : IRxPancakeView
        //{
        //    var gradientStopsCollection = new GradientStopCollection();
        //    foreach (var gradientStop in gradientStops)
        //        gradientStopsCollection.Add(gradientStop);
        //    pancakeview.BorderGradientStops = gradientStopsCollection;
        //    return pancakeview;
        //}

        public static T OffsetAngle<T>(this T pancakeview, double offsetAngle) where T : IRxPancakeView
        {
            pancakeview.OffsetAngle = offsetAngle;
            return pancakeview;
        }

        public static T Shadow<T>(this T pancakeview, DropShadow shadow) where T : IRxPancakeView
        {
            pancakeview.Shadow = shadow;
            return pancakeview;
        }
    }
}