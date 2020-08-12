using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using XamarinReactorUI.Animations;
using XamarinReactorUI.Shapes;

namespace XamarinReactorUI
{
    public interface IRxVisualElement : IRxElement
    {
        //Style Style { get; set; }
        bool InputTransparent { get; set; }
        bool IsEnabled { get; set; }
        double AnchorX { get; set; }
        double AnchorY { get; set; }
        double TranslationX { get; set; }
        double TranslationY { get; set; }
        double Rotation { get; set; }
        double RotationX { get; set; }
        double RotationY { get; set; }
        double Scale { get; set; }
        double ScaleX { get; set; }
        double ScaleY { get; set; }
        //IRxGeometry Clip { get; set; }
        IVisual Visual { get; set; }
        bool IsVisible { get; set; }
        double Opacity { get; set; }
        Color BackgroundColor { get; set; }
        double WidthRequest { get; set; }
        double HeightRequest { get; set; }
        double MinimumWidthRequest { get; set; }
        double MinimumHeightRequest { get; set; }
        FlowDirection FlowDirection { get; set; }
        int TabIndex { get; set; }
        bool IsTabStop { get; set; }
        VisualStateGroupList VisualStateGroups { get; set; }
    }

    public abstract class RxVisualElement<T> : RxElement<T>, IRxVisualElement where T : Xamarin.Forms.VisualElement, new()
    {
        protected RxVisualElement()
        {
        }

        protected RxVisualElement(Action<T> componentRefAction)
            : base(componentRefAction)
        {

        }

        //public Style Style { get; set; } = (Style)VisualElement.StyleProperty.DefaultValue;
        public bool InputTransparent { get; set; } = (bool)VisualElement.InputTransparentProperty.DefaultValue;
        public bool IsEnabled { get; set; } = (bool)VisualElement.IsEnabledProperty.DefaultValue;
        public double AnchorX { get; set; } = (double)VisualElement.AnchorXProperty.DefaultValue;
        public double AnchorY { get; set; } = (double)VisualElement.AnchorYProperty.DefaultValue;
        public double TranslationX { get; set; } = (double)VisualElement.TranslationXProperty.DefaultValue;
        public double TranslationY { get; set; } = (double)VisualElement.TranslationYProperty.DefaultValue;
        public double Rotation { get; set; } = (double)VisualElement.RotationProperty.DefaultValue;
        public double RotationX { get; set; } = (double)VisualElement.RotationXProperty.DefaultValue;
        public double RotationY { get; set; } = (double)VisualElement.RotationYProperty.DefaultValue;
        public double Scale { get; set; } = (double)VisualElement.ScaleProperty.DefaultValue;
        public double ScaleX { get; set; } = (double)VisualElement.ScaleXProperty.DefaultValue;
        public double ScaleY { get; set; } = (double)VisualElement.ScaleYProperty.DefaultValue;
        public IVisual Visual { get; set; } = (IVisual)VisualElement.VisualProperty.DefaultValue;
        public bool IsVisible { get; set; } = (bool)VisualElement.IsVisibleProperty.DefaultValue;
        public double Opacity { get; set; } = (double)VisualElement.OpacityProperty.DefaultValue;
        public Color BackgroundColor { get; set; } = (Color)VisualElement.BackgroundColorProperty.DefaultValue;
        public double WidthRequest { get; set; } = (double)VisualElement.WidthRequestProperty.DefaultValue;
        public double HeightRequest { get; set; } = (double)VisualElement.HeightRequestProperty.DefaultValue;
        public double MinimumWidthRequest { get; set; } = (double)VisualElement.MinimumWidthRequestProperty.DefaultValue;
        public double MinimumHeightRequest { get; set; } = (double)VisualElement.MinimumHeightRequestProperty.DefaultValue;
        public FlowDirection FlowDirection { get; set; } = (FlowDirection)VisualElement.FlowDirectionProperty.DefaultValue;
        public int TabIndex { get; set; } = (int)VisualElement.TabIndexProperty.DefaultValue;
        public bool IsTabStop { get; set; } = (bool)VisualElement.IsTabStopProperty.DefaultValue;
        //public IRxGeometry Clip { get; set; }

        public VisualStateGroupList VisualStateGroups { get; set; } = new VisualStateGroupList();

        protected override void OnUpdate()
        {
            //NativeControl.Style = Style ?? Application.Current.Resources.GetResourceOrDefault<Style>(typeof(T).ToString());
            NativeControl.InputTransparent = InputTransparent;
            NativeControl.IsEnabled = IsEnabled;
            NativeControl.AnchorX = AnchorX;
            NativeControl.AnchorY = AnchorY;
            NativeControl.TranslationX = TranslationX;
            NativeControl.TranslationY = TranslationY;
            NativeControl.Rotation = Rotation;
            NativeControl.RotationX = RotationX;
            NativeControl.RotationY = RotationY;
            NativeControl.Scale = Scale;
            NativeControl.ScaleX = ScaleX;
            NativeControl.ScaleY = ScaleY;
            NativeControl.Visual = Visual;
            NativeControl.IsVisible = IsVisible;
            NativeControl.Opacity = Opacity;
            NativeControl.BackgroundColor = BackgroundColor;
            NativeControl.WidthRequest = WidthRequest;
            NativeControl.HeightRequest = HeightRequest;
            NativeControl.MinimumWidthRequest = MinimumWidthRequest;
            NativeControl.MinimumHeightRequest = MinimumHeightRequest;
            NativeControl.FlowDirection = FlowDirection;
            NativeControl.TabIndex = TabIndex;
            NativeControl.IsTabStop = IsTabStop;

            //NativeControl.Clip = (Clip as IVisualNodeWithNativeControl)?.GetNativeControl<Geometry>();

            //TODO: Merge instead of clear+add
            VisualStateManager.SetVisualStateGroups(NativeControl, VisualStateGroups);

            base.OnUpdate();
        }

        protected override void OnAnimate()
        {
            NativeControl.AnchorX = AnchorX;
            NativeControl.AnchorY = AnchorY;
            NativeControl.TranslationX = TranslationX;
            NativeControl.TranslationY = TranslationY;
            NativeControl.Rotation = Rotation;
            NativeControl.RotationX = RotationX;
            NativeControl.RotationY = RotationY;
            NativeControl.Scale = Scale;
            NativeControl.ScaleX = ScaleX;
            NativeControl.ScaleY = ScaleY;
            NativeControl.Opacity = Opacity;
            NativeControl.BackgroundColor = BackgroundColor;
            NativeControl.WidthRequest = WidthRequest;
            NativeControl.HeightRequest = HeightRequest;
            NativeControl.MinimumWidthRequest = MinimumWidthRequest;
            NativeControl.MinimumHeightRequest = MinimumHeightRequest;

            //NativeControl.Clip = (Clip as IVisualNodeWithNativeControl)?.GetNativeControl<Geometry>();
            //if (NativeControl.Clip != null)
            //{
            //    //System.Diagnostics.Debug.WriteLine($"RxEllipseGeometry()=>RadiusX={((EllipseGeometry)NativeControl.Clip).RadiusX} RadiusY={((EllipseGeometry)NativeControl.Clip).RadiusY}");
            //}

            base.OnAnimate();
        }

        internal override void Layout(RxTheme theme = null, VisualNode parent = null)
        {
            //(Clip as VisualNode)?.Layout(theme, this);

            base.Layout(theme, parent);
        }

        internal override bool Animate()
        {
            var animate = base.Animate();
            //if (((Clip as VisualNode)?.Animate()).GetValueOrDefault())
            //{
            //    var clip = NativeControl.Clip;
            //    NativeControl.Clip = null;
            //    NativeControl.Clip = clip;
            //    //System.Diagnostics.Debug.WriteLine($"RxEllipseGeometry()=>RadiusX={((EllipseGeometry)NativeControl.Clip).RadiusX} RadiusY={((EllipseGeometry)NativeControl.Clip).RadiusY}");
            //    //System.Diagnostics.Debug.WriteLine($"RxEllipseGeometry()=>CenterX={((EllipseGeometry)NativeControl.Clip).Center.X} CenterY={((EllipseGeometry)NativeControl.Clip).Center.Y}");
            //    animate = true;
            //}

            return animate;
        }

        //protected override void OnMigrated(VisualNode newNode)
        //{
        //    var newElement = (IRxVisualElement)newNode;
        //    if (Clip != null &&
        //        newElement.Clip != null &&
        //        Clip.GetType() == newElement.Clip.GetType()) 
        //    {
        //        (Clip as VisualNode).MergeWith((VisualNode)newElement.Clip);
        //    }

        //    base.OnMigrated(newNode);
        //}

    }

    public class VisualStateNamedGroup
    {
        public const string Common = "CommonStates";
    }

    public static class RxVisualElementExtensions
    {
        public static T VisualState<T>(this T visualElement, string groupName, string stateName, BindableProperty property, object value, string targetName = null) where T : IRxVisualElement
        {
            var group = visualElement.VisualStateGroups.FirstOrDefault(_ => _.Name == groupName);

            if (group == null)
            {
                visualElement.VisualStateGroups.Add(group = new VisualStateGroup()
                {
                    Name = groupName
                });
            }

            var state = group.States.FirstOrDefault(_ => _.Name == stateName);
            if (state == null)
            {
                group.States.Add(state = new VisualState { Name = stateName });
            }

            state.Setters.Add(new Setter() { Property = property, Value = value });

            return visualElement;
        }

        public static T InputTransparent<T>(this T visualelement, bool inputTransparent) where T : IRxVisualElement
        {
            visualelement.InputTransparent = inputTransparent;
            return visualelement;
        }
        public static T IsEnabled<T>(this T visualelement, bool isEnabled) where T : IRxVisualElement
        {
            visualelement.IsEnabled = isEnabled;
            return visualelement;
        }
        public static T AnchorX<T>(this T visualelement, double anchorX, RxDoubleAnimation customAnimation = null) where T : IRxVisualElement
        {
            visualelement.AnchorX = anchorX;
            visualelement.AppendAnimatable(VisualElement.AnchorXProperty, customAnimation ?? new RxDoubleAnimation(anchorX), v => visualelement.AnchorX = v.CurrentValue());
            return visualelement;
        }
        public static T AnchorY<T>(this T visualelement, double anchorY, RxDoubleAnimation customAnimation = null) where T : IRxVisualElement
        {
            visualelement.AnchorY = anchorY;
            visualelement.AppendAnimatable(VisualElement.AnchorYProperty, customAnimation ?? new RxDoubleAnimation(anchorY), v => visualelement.AnchorY = v.CurrentValue());
            return visualelement;
        }
        public static T TranslationX<T>(this T visualelement, double translationX, RxDoubleAnimation customAnimation = null) where T : IRxVisualElement
        {
            visualelement.TranslationX = translationX;
            visualelement.AppendAnimatable(VisualElement.TranslationXProperty, customAnimation ?? new RxDoubleAnimation(translationX), v => visualelement.TranslationX = v.CurrentValue());
            return visualelement;
        }
        public static T TranslationY<T>(this T visualelement, double translationY, RxDoubleAnimation customAnimation = null) where T : IRxVisualElement
        {
            visualelement.TranslationY = translationY;
            visualelement.AppendAnimatable(VisualElement.TranslationXProperty, customAnimation ?? new RxDoubleAnimation(translationY), v => visualelement.TranslationY = v.CurrentValue());
            return visualelement;
        }
        public static T Rotation<T>(this T visualelement, double rotation, RxDoubleAnimation customAnimation = null) where T : IRxVisualElement
        {
            visualelement.Rotation = rotation;
            visualelement.AppendAnimatable(VisualElement.RotationProperty, customAnimation ?? new RxDoubleAnimation(rotation), v => visualelement.Rotation = v.CurrentValue());
            return visualelement;
        }
        public static T RotationX<T>(this T visualelement, double rotationX, RxDoubleAnimation customAnimation = null) where T : IRxVisualElement
        {
            visualelement.RotationX = rotationX;
            visualelement.AppendAnimatable(VisualElement.RotationXProperty, customAnimation ?? new RxDoubleAnimation(rotationX), v => visualelement.RotationX = v.CurrentValue());
            return visualelement;
        }
        public static T RotationY<T>(this T visualelement, double rotationY, RxDoubleAnimation customAnimation = null) where T : IRxVisualElement
        {
            visualelement.RotationY = rotationY;
            visualelement.AppendAnimatable(VisualElement.RotationYProperty, customAnimation ?? new RxDoubleAnimation(rotationY), v => visualelement.RotationY = v.CurrentValue());
            return visualelement;
        }
        public static T Scale<T>(this T visualelement, double scale, RxDoubleAnimation customAnimation = null) where T : IRxVisualElement
        {
            visualelement.Scale = scale;
            visualelement.AppendAnimatable(VisualElement.ScaleProperty, customAnimation ?? new RxDoubleAnimation(scale), v => visualelement.Scale = v.CurrentValue());
            return visualelement;
        }

        public static T ScaleX<T>(this T visualelement, double scaleX, RxDoubleAnimation customAnimation = null) where T : IRxVisualElement
        {
            visualelement.ScaleX = scaleX;
            visualelement.AppendAnimatable(VisualElement.ScaleXProperty, customAnimation ?? new RxDoubleAnimation(scaleX), v => visualelement.ScaleX = v.CurrentValue());
            return visualelement;
        }

        public static T ScaleY<T>(this T visualelement, double scaleY, RxDoubleAnimation customAnimation = null) where T : IRxVisualElement
        {
            visualelement.ScaleY = scaleY;
            visualelement.AppendAnimatable(VisualElement.ScaleYProperty, customAnimation ?? new RxDoubleAnimation(scaleY), v => visualelement.ScaleY = v.CurrentValue());
            return visualelement;
        }

        public static T Visual<T>(this T visualelement, IVisual visual) where T : IRxVisualElement
        {
            visualelement.Visual = visual;
            return visualelement;
        }
        public static T IsVisible<T>(this T visualelement, bool isVisible) where T : IRxVisualElement
        {
            visualelement.IsVisible = isVisible;
            visualelement.AppendAnimatable(VisualElement.IsVisibleProperty, new RxDoubleAnimation(isVisible ? 1.0 : 0.0), v => visualelement.Opacity = v.CurrentValue());
            return visualelement;
        }
        public static T Opacity<T>(this T visualelement, double opacity, RxDoubleAnimation customAnimation = null) where T : IRxVisualElement
        {
            visualelement.Opacity = opacity;
            visualelement.AppendAnimatable(VisualElement.OpacityProperty, customAnimation ?? new RxDoubleAnimation(opacity), v => visualelement.Opacity = v.CurrentValue());
            return visualelement;
        }
        public static T BackgroundColor<T>(this T visualelement, Color backgroundColor, RxColorAnimation customAnimation = null) where T : IRxVisualElement
        {
            visualelement.BackgroundColor = backgroundColor;
            visualelement.AppendAnimatable(VisualElement.WidthRequestProperty, customAnimation ?? new RxSimpleColorAnimation(backgroundColor), v => visualelement.BackgroundColor = v.CurrentValue());
            return visualelement;
        }
        public static T WidthRequest<T>(this T visualelement, double widthRequest, RxDoubleAnimation customAnimation = null) where T : IRxVisualElement
        {
            visualelement.WidthRequest = widthRequest;
            visualelement.AppendAnimatable(VisualElement.WidthRequestProperty, customAnimation ?? new RxDoubleAnimation(widthRequest), v => visualelement.WidthRequest = v.CurrentValue());
            return visualelement;
        }
        public static T HeightRequest<T>(this T visualelement, double heightRequest, RxDoubleAnimation customAnimation = null) where T : IRxVisualElement
        {
            visualelement.HeightRequest = heightRequest;
            visualelement.AppendAnimatable(VisualElement.HeightRequestProperty, customAnimation ?? new RxDoubleAnimation(heightRequest), v => visualelement.HeightRequest = v.CurrentValue());
            return visualelement;
        }
        public static T MinimumWidthRequest<T>(this T visualelement, double minimumWidthRequest, RxDoubleAnimation customAnimation = null) where T : IRxVisualElement
        {
            visualelement.MinimumWidthRequest = minimumWidthRequest;
            visualelement.AppendAnimatable(VisualElement.MinimumWidthRequestProperty, customAnimation ?? new RxDoubleAnimation(minimumWidthRequest), v => visualelement.MinimumWidthRequest = v.CurrentValue());
            return visualelement;
        }
        public static T MinimumHeightRequest<T>(this T visualelement, double minimumHeightRequest, RxDoubleAnimation customAnimation = null) where T : IRxVisualElement
        {
            visualelement.MinimumHeightRequest = minimumHeightRequest;
            visualelement.AppendAnimatable(VisualElement.MinimumHeightRequestProperty, customAnimation ?? new RxDoubleAnimation(minimumHeightRequest), v => visualelement.MinimumHeightRequest = v.CurrentValue());
            return visualelement;
        }
        public static T FlowDirection<T>(this T visualelement, FlowDirection flowDirection) where T : IRxVisualElement
        {
            visualelement.FlowDirection = flowDirection;
            return visualelement;
        }
        public static T TabIndex<T>(this T visualelement, int tabIndex) where T : IRxVisualElement
        {
            visualelement.TabIndex = tabIndex;
            return visualelement;
        }
        public static T IsTabStop<T>(this T visualelement, bool isTabStop) where T : IRxVisualElement
        {
            visualelement.IsTabStop = isTabStop;
            return visualelement;
        }
        
        public static T Clip<T>(this T visualelement, IRxGeometry geometry) where T : IRxVisualElement
        {
            //visualelement.Clip = geometry;
            return visualelement;
        }
    }

}
