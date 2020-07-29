using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinReactorUI.TestApp.Shell.Test4
{
    class Page2 : RxComponent
    {
        public override VisualNode Render()
        {
            return new RxContentPage()
            {
                new RxLabel("Page 2")
                    .HCenter()
                    .VCenter()
            };
        }

    }
}
