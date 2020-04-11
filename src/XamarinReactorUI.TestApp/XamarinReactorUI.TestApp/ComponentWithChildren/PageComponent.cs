using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XamarinReactorUI.TestApp.ComponentWithChildren
{
    public class PageState : IState
    {
        public int ColumnCount { get; set; } = 1;

        public int ItemCount { get; set; } = 3;
    }

    public class PageComponent : RxComponent<PageState>
    {
        public override VisualNode Render()
        {
            return new RxNavigationPage()
            {
                new RxContentPage()
                {
                    new RxStackLayout()
                    { 
                        new RxLabel($"Columns {State.ColumnCount}")
                            .FontSize(Xamarin.Forms.NamedSize.Large),
                        new RxStepper()
                            .Minimum(1)
                            .Maximum(10)
                            .Increment(1)
                            .Value(State.ColumnCount)
                            .OnValueChanged(_=> SetState(s => s.ColumnCount = (int)_.NewValue)),
                        new RxLabel($"Items {State.ItemCount}")
                            .FontSize(Xamarin.Forms.NamedSize.Large),
                        new RxStepper()
                            .Minimum(1)
                            .Maximum(20)
                            .Increment(1)
                            .Value(State.ItemCount)
                            .OnValueChanged(_=> SetState(s => s.ItemCount = (int)_.NewValue)),

                        new WrapGrid()
                        { 
                            Enumerable.Range(1, State.ItemCount)
                                .Select(_=> new RxButton($"Item {_}"))
                                .ToArray()
                        }
                        .ColumnCount(State.ColumnCount)                            
                    }
                    .Padding(10)
                    .WithVerticalOrientation()
                }
                .Title("Component With Children")
            };
        }
    }

}
