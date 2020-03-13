using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxVisualElement
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

    public abstract class RxVisualElement<T> : RxElement, IRxVisualElement where T : Xamarin.Forms.VisualElement, new()
    {
        private readonly Action<T> _componentRefAction;

        protected RxVisualElement()
        {
        }

        protected RxVisualElement(Action<T> componentRefAction)
        {
            _componentRefAction = componentRefAction;
        }

        protected T NativeControl { get => (T)_nativeControl; }

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

            //TODO: Merge instead of clear+add
            VisualStateManager.SetVisualStateGroups(NativeControl, VisualStateGroups);

            base.OnUpdate();
        }

        protected override void OnMount()
        {
            _nativeControl = _nativeControl ?? new T();
            Parent.AddChild(this, NativeControl);
            _componentRefAction?.Invoke(NativeControl);

            base.OnMount();
        }

        protected override void OnUnmount()
        {
            Parent.RemoveChild(this, NativeControl);
            _nativeControl = null;
            _componentRefAction?.Invoke(null);

            base.OnUnmount();
        }
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

        //public static T Style<T>(this T visualelement, Style style) where T : IRxVisualElement
        //{
        //    visualelement.Style = style;
        //    return visualelement;
        //}
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
        public static T AnchorX<T>(this T visualelement, double anchorX) where T : IRxVisualElement
        {
            visualelement.AnchorX = anchorX;
            return visualelement;
        }
        public static T AnchorY<T>(this T visualelement, double anchorY) where T : IRxVisualElement
        {
            visualelement.AnchorY = anchorY;
            return visualelement;
        }
        public static T TranslationX<T>(this T visualelement, double translationX) where T : IRxVisualElement
        {
            visualelement.TranslationX = translationX;
            return visualelement;
        }
        public static T TranslationY<T>(this T visualelement, double translationY) where T : IRxVisualElement
        {
            visualelement.TranslationY = translationY;
            return visualelement;
        }
        public static T Rotation<T>(this T visualelement, double rotation) where T : IRxVisualElement
        {
            visualelement.Rotation = rotation;
            return visualelement;
        }
        public static T RotationX<T>(this T visualelement, double rotationX) where T : IRxVisualElement
        {
            visualelement.RotationX = rotationX;
            return visualelement;
        }
        public static T RotationY<T>(this T visualelement, double rotationY) where T : IRxVisualElement
        {
            visualelement.RotationY = rotationY;
            return visualelement;
        }
        public static T Scale<T>(this T visualelement, double scale) where T : IRxVisualElement
        {
            visualelement.Scale = scale;
            return visualelement;
        }
        public static T ScaleX<T>(this T visualelement, double scaleX) where T : IRxVisualElement
        {
            visualelement.ScaleX = scaleX;
            return visualelement;
        }
        public static T ScaleY<T>(this T visualelement, double scaleY) where T : IRxVisualElement
        {
            visualelement.ScaleY = scaleY;
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
            return visualelement;
        }
        public static T Opacity<T>(this T visualelement, double opacity) where T : IRxVisualElement
        {
            visualelement.Opacity = opacity;
            return visualelement;
        }
        public static T BackgroundColor<T>(this T visualelement, Color backgroundColor) where T : IRxVisualElement
        {
            visualelement.BackgroundColor = backgroundColor;
            return visualelement;
        }
        public static T WidthRequest<T>(this T visualelement, double widthRequest) where T : IRxVisualElement
        {
            visualelement.WidthRequest = widthRequest;
            return visualelement;
        }
        public static T HeightRequest<T>(this T visualelement, double heightRequest) where T : IRxVisualElement
        {
            visualelement.HeightRequest = heightRequest;
            return visualelement;
        }
        public static T MinimumWidthRequest<T>(this T visualelement, double minimumWidthRequest) where T : IRxVisualElement
        {
            visualelement.MinimumWidthRequest = minimumWidthRequest;
            return visualelement;
        }
        public static T MinimumHeightRequest<T>(this T visualelement, double minimumHeightRequest) where T : IRxVisualElement
        {
            visualelement.MinimumHeightRequest = minimumHeightRequest;
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
    }

}
