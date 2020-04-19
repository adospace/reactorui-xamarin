using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public class RxDataTemplate<T> : VisualNode where T : new()
    {
        private DataTemplate _dataTemplate = null;

        public RxDataTemplate()
        { }

        

        protected override void OnMount()
        {
            //_dataTemplate = _dataTemplate ?? new DataTemplate()
            base.OnMount();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }
}
