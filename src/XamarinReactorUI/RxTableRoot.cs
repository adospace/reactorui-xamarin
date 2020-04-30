//using System;
//using System.Collections.Generic;
//using Xamarin.Forms;

//namespace XamarinReactorUI
//{
//    public interface IRxTableRoot
//    {
//    }

//    public class RxTableRoot<T> : RxView<T>, IRxTableRoot where T : TableRoot, new()
//    {
//        public RxTableRoot()
//        {
//        }

//        public RxTableRoot(Action<T> componentRefAction)
//            : base(componentRefAction)
//        {
//        }

//        protected override void OnUpdate()
//        {
            
//            base.OnUpdate();
//        }

//        protected override IEnumerable<VisualNode> RenderChildren()
//        {
//            yield break;
//        }
//    }

//    public class RxTableRoot : RxTableRoot<Xamarin.Forms.TableRoot>
//    {
//        public RxTableRoot()
//        {
//        }

//        public RxTableRoot(Action<TableRoot> componentRefAction)
//            : base(componentRefAction)
//        {
//        }
//    }

//    public static class RxTableRootExtensions
//    {
//    }
//}