using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinReactorUI
{
    public class VisualTree
    {
        public VisualTree(VisualNode root)
        {
            Root = root ?? throw new ArgumentNullException(nameof(root));
        }

        public VisualNode Root { get; }

        public void Layout()
        {
            Root.Layout();
        }

    }
}
