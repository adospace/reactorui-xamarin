using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinReactorUI
{
    public abstract class RxVisualElement : RxElement
    {
        protected RxVisualElement()
        {
        }

        public bool IsEnabled { get; set; }
    }

    public static class RxVisualElementExtensions
    {
        public static T IsEnabled<T>(this T element, bool enabled) where T : RxVisualElement
        {
            element.IsEnabled = enabled;
            return element;
        }
    
    }
}
