using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using XamarinReactorUI.TestApp.CollectionView;

namespace XamarinReactorUI.TestApp.Shell.Test3
{
    public class ShellWithSearchState: IState
    {
        public IReadOnlyList<Monkey> Monkeys { get; set; }
    }  
    
    public class ShellWithSearch : RxComponent<ShellWithSearchState>
    {
        protected override void OnMounted()
        {
            State.Monkeys = Monkey.GetList();

            base.OnMounted();
        }
        public override VisualNode Render()
        {
            return new RxShell()
            {
                new RxContentPage()
                {
                    new RxSearchHandler<Monkey>()
                    {
                        Results = State.Monkeys,
                        Template = (monkey) => RenderMonkeyItem(monkey)
                    }
                    .ShowsResults(true)
                    .Placeholder("Search monkeys...")
                    .OnQueryChanged(OnQueryList)
                }
            };
        }

        private void OnQueryList(string query)
        {
            SetState(s => s.Monkeys = Monkey.GetList().Where(monkey => monkey.Name.ToLower().Contains(query.ToLower())).ToList());
        }

        private VisualNode RenderMonkeyItem(Monkey monkey)
        {
            return new RxGrid("25 25", "* 3*")
            {
                new RxImage()
                    .Source(new Uri(monkey.ImageUrl))
                    .Aspect(Xamarin.Forms.Aspect.AspectFill)
                    .GridRowSpan(2),

                new RxLabel(monkey.Name)
                    .GridColumn(1),
                new RxLabel(monkey.Details)
                    .GridRow(1)
                    .GridColumn(1)
            }
            .Padding(3,0)
            .RowSpacing(4)
            .ColumnSpacing(2);
        }
    }

}
