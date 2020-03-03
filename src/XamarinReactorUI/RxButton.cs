using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxButton
    { 
        string Text { get; set; }
        Action ClickAction { get; set; }
    }

    public class RxButton : RxView<Button>, IRxButton
    {
        public RxButton(string text = null)
        {
            Text = text;
        }

        public string Text { get; set; }

        public Action ClickAction { get; set; }

        protected override void OnUpdate()
        {
            NativeControl.Text = Text;
            if (ClickAction != null)
                NativeControl.Clicked += NativeControl_Clicked;

            base.OnUpdate();
        }

        private void NativeControl_Clicked(object sender, EventArgs e)
        {
            ClickAction?.Invoke();
        }

        protected override void OnMigrated()
        {
            NativeControl.Clicked -= NativeControl_Clicked;

            base.OnMigrated();
        }

        protected override void OnUnmount()
        {
            NativeControl.Clicked -= NativeControl_Clicked;
            base.OnUnmount();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }


    public static class RxButtonViewExtensions
    {
        public static T Text<T>(this T button, string text) where T : RxButton
        {
            button.Text = text;
            return button;
        }

        public static T OnClick<T>(this T button, Action clickAction) where T : RxButton
        {
            button.ClickAction = clickAction;
            return button;
        }

    }
}
