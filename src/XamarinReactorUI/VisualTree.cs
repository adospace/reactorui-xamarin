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
            Root.LayoutCycleRequest += (s, e) => LayoutCycleRequest?.Invoke(this, EventArgs.Empty);
        }

        public VisualNode Root { get; }

        public void Layout()
        {
            Root.Layout();
        }

        public event EventHandler LayoutCycleRequest;

    }
}
