using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public abstract class RxElement : VisualNode
    {
        protected Xamarin.Forms.Element _nativeControl;

        internal override void MergeWith(VisualNode newNode)
        {
            if (newNode.GetType() == GetType())
            {
                ((RxElement)newNode)._nativeControl = this._nativeControl;
                ((RxElement)newNode)._isMounted = this._nativeControl != null;
                this.OnMigrated();
            }

            base.MergeWith(newNode);
        }

        protected virtual void OnMigrated()
        { }

        protected override void OnUnmount()
        {
            

            base.OnUnmount();
        }

        protected override void OnUpdate()
        {


            base.OnUpdate();
        }

    }


}
