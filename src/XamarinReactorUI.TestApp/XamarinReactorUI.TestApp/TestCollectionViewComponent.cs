using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XamarinReactorUI.TestApp
{
    public class TestCollectionViewComponent : RxComponent
    {
        private readonly List<Person> _largePersonList = new List<Person>()
        {
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
            new Person { Name = "Steve", Age = 21, Location = "USA" },
        };

        private readonly List<Person> _smallPersonList = new List<Person>()
        {
            new Person { Name = "John", Age = 22, Location = "USA" },
            new Person { Name = "John", Age = 21, Location = "USA" },
            new Person { Name = "John", Age = 21, Location = "USA" },
        };

        bool _largePersonListVisible = false;
        private void OnShowHideLargeList()
        {
            _largePersonListVisible = !_largePersonListVisible;
            Invalidate();
        }

        private IEnumerable<VisualNode> RenderList(IEnumerable<Person> persons)
        {
            return persons.Select((p) =>
            {
                return new RxStackLayout()
                            {
                                new RxLabel(p.Name)
                                    .FontSize(Xamarin.Forms.NamedSize.Default)
                                    .Margin(5),
                                new RxLabel(p.Location)
                                    .Margin(5),
                            }
                .Padding(10)
                ;
            });
        }

        public override VisualNode Render()
        {
            return 
                new RxStackLayout()
                {
                    new RxButton("SWITCH SMALL/LARGE LIST")
                        .OnClick(OnShowHideLargeList),
                        _largePersonListVisible ? 
                        new RxCollectionView(RenderList(_largePersonList))
                        .VFillAndExpand()
                        :
                        new RxCollectionView(RenderList(_smallPersonList))
                        .VFillAndExpand()
                }
                .HFillAndExpand()
                .VFillAndExpand();
        }

    }
}
