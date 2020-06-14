using Plugin.SharedTransitions;
using System;

namespace XamarinReactorUI.SharedTransitions
{
    public interface IRxSharedTransitionNavigationPage
    {
    }

    public class RxSharedTransitionNavigationPage<T> : RxNavigationPage<T>, IRxSharedTransitionNavigationPage where T : SharedTransitionNavigationPage, new()
    {
        public RxSharedTransitionNavigationPage()
        {
        }

        public RxSharedTransitionNavigationPage(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public class RxSharedTransitionNavigationPage : RxSharedTransitionNavigationPage<SharedTransitionNavigationPage>
    {
        public RxSharedTransitionNavigationPage()
        {
        }

        public RxSharedTransitionNavigationPage(Action<SharedTransitionNavigationPage> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public static class RxSharedTransitionNavigationPageExtensions
    {
        public static T TransitionSelectedGroup<T>(this T page, string value) where T : IRxPage
        {
            page.SetAttachedProperty(SharedTransitionNavigationPage.TransitionSelectedGroupProperty, value);
            return page;
        }

        public static T BackgroundAnimation<T>(this T page, BackgroundAnimation value) where T : IRxPage
        {
            page.SetAttachedProperty(SharedTransitionNavigationPage.BackgroundAnimationProperty, value);
            return page;
        }

        public static T TransitionDuration<T>(this T page, long value) where T : IRxPage
        {
            page.SetAttachedProperty(SharedTransitionNavigationPage.TransitionDurationProperty, value);
            return page;
        }

        public static T TransitionName<T>(this T element, string name) where T : IRxElement
        {
            element.SetAttachedProperty(Transition.NameProperty, name);
            return element;
        }

        public static T TransitionGroup<T>(this T element, string name) where T : IRxElement
        {
            element.SetAttachedProperty(Transition.GroupProperty, name);
            return element;
        }
    }

}
