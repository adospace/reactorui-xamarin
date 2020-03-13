
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxImage
    {
        ImageSource Source { get; set; }
        Aspect Aspect { get; set; }
        bool IsOpaque { get; set; }
        bool IsAnimationPlaying { get; set; }
    }

    public sealed class RxImage : RxView<Image>, IRxImage
    {
        public RxImage()
        {

        }

        public RxImage(Action<Image> componentRefAction)
            : base(componentRefAction)
        {

        }

        public ImageSource Source { get; set; } = (ImageSource)Image.SourceProperty.DefaultValue;
        public Aspect Aspect { get; set; } = (Aspect)Image.AspectProperty.DefaultValue;
        public bool IsOpaque { get; set; } = (bool)Image.IsOpaqueProperty.DefaultValue;
        public bool IsAnimationPlaying { get; set; } = (bool)Image.IsAnimationPlayingProperty.DefaultValue;

        protected override void OnUpdate()
        {
            NativeControl.Source = Source;
            NativeControl.Aspect = Aspect;
            NativeControl.IsOpaque = IsOpaque;
            NativeControl.IsAnimationPlaying = IsAnimationPlaying;

            base.OnUpdate();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }

    public static class RxImageExtensions
    {
        public static T Source<T>(this T image, ImageSource source) where T : IRxImage
        {
            image.Source = source;
            return image;
        }


        public static T Source<T>(this T image, string file) where T : IRxImage
        {
            image.Source = ImageSource.FromFile(file);
            return image;
        }
        public static T Source<T>(this T image, string fileAndroid, string fileiOS) where T : IRxImage
        {
            image.Source = Device.RuntimePlatform == Device.Android ? ImageSource.FromFile(fileAndroid) : ImageSource.FromFile(fileiOS);
            return image;
        }
        public static T Source<T>(this T image, string resourceName, Assembly sourceAssembly) where T : IRxImage
        {
            image.Source = ImageSource.FromResource(resourceName, sourceAssembly);
            return image;
        }
        public static T Source<T>(this T image, Uri imageUri) where T : IRxImage
        {
            image.Source = ImageSource.FromUri(imageUri);
            return image;
        }
        public static T Source<T>(this T image, Uri imageUri, bool cachingEnabled, TimeSpan cacheValidity) where T : IRxImage
        {
            image.Source = new UriImageSource
            {
                Uri = imageUri,
                CachingEnabled = cachingEnabled,
                CacheValidity = cacheValidity
            };
            return image;
        }
        public static T Source<T>(this T image, Func<Stream> imageStream) where T : IRxImage
        {
            image.Source = ImageSource.FromStream(imageStream);
            return image;
        }


        public static T Aspect<T>(this T image, Aspect aspect) where T : IRxImage
        {
            image.Aspect = aspect;
            return image;
        }



        public static T IsOpaque<T>(this T image, bool isOpaque) where T : IRxImage
        {
            image.IsOpaque = isOpaque;
            return image;
        }



        public static T IsAnimationPlaying<T>(this T image, bool isAnimationPlaying) where T : IRxImage
        {
            image.IsAnimationPlaying = isAnimationPlaying;
            return image;
        }



    }

}
