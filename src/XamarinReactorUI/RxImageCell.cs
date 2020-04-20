using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxImageCell : IRxTextCell
    {
        ImageSource ImageSource { get; set; }
    }

    public class RxImageCell<T> : RxTextCell<T>, IRxImageCell where T : ImageCell, new()
    {
        public RxImageCell()
        {
        }

        public RxImageCell(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public ImageSource ImageSource { get; set; } = (ImageSource)ImageCell.ImageSourceProperty.DefaultValue;

        protected override void OnUpdate()
        {
            NativeControl.ImageSource = ImageSource;

            base.OnUpdate();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }

    public class RxImageCell : RxImageCell<ImageCell>
    {
        internal RxImageCell(ImageCell imageCell)
        {
            _nativeControl = imageCell;
        }

        protected internal override void OnLayoutCycleRequested()
        {
            Layout();
            base.OnLayoutCycleRequested();
        }
    }

    public static class RxImageCellExtensions
    {
        public static T ImageSource<T>(this T imagecell, ImageSource imageSource) where T : IRxImageCell
        {
            imagecell.ImageSource = imageSource;
            return imagecell;
        }

        public static T Image<T>(this T imagecell, string file) where T : IRxImageCell
        {
            imagecell.ImageSource = Xamarin.Forms.ImageSource.FromFile(file);
            return imagecell;
        }

        public static T Image<T>(this T imagecell, string fileAndroid, string fileiOS) where T : IRxImageCell
        {
            imagecell.ImageSource = Device.RuntimePlatform == Device.Android ? Xamarin.Forms.ImageSource.FromFile(fileAndroid) : Xamarin.Forms.ImageSource.FromFile(fileiOS);
            return imagecell;
        }

        public static T Image<T>(this T imagecell, string resourceName, Assembly sourceAssembly) where T : IRxImageCell
        {
            imagecell.ImageSource = Xamarin.Forms.ImageSource.FromResource(resourceName, sourceAssembly);
            return imagecell;
        }

        public static T Image<T>(this T imagecell, Uri imageUri) where T : IRxImageCell
        {
            imagecell.ImageSource = Xamarin.Forms.ImageSource.FromUri(imageUri);
            return imagecell;
        }

        public static T Image<T>(this T imagecell, Uri imageUri, bool cachingEnabled, TimeSpan cacheValidity) where T : IRxImageCell
        {
            imagecell.ImageSource = new UriImageSource
            {
                Uri = imageUri,
                CachingEnabled = cachingEnabled,
                CacheValidity = cacheValidity
            };
            return imagecell;
        }

        public static T Image<T>(this T imagecell, Func<Stream> imageStream) where T : IRxImageCell
        {
            imagecell.ImageSource = Xamarin.Forms.ImageSource.FromStream(imageStream);
            return imagecell;
        }
    }
}