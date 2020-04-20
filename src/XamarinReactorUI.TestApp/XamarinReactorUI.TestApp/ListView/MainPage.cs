using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinReactorUI;
using XamarinReactorUI.TestApp.CollectionView;

namespace XamarinReactorUI.TestApp.ListView
{
    public enum ViewMode
    {
        TextCell,

        ImageCell,

        SwitchCell,

        EntryCell,

        ViewCell
    }

    public class MainPageState : IState
    {
        public ViewMode ViewMode { get; set; } = ViewMode.ViewCell;
    }

    public class MainPage : RxComponent<MainPageState>
    {
        private readonly IEnumerable<Monkey> _allMonkeys = Monkey.GetList();

        private VisualNode RenderSwitchMode(ViewMode viewMode)
            => new RxStackLayout()
            {
                new RxLabel(viewMode.ToString()),
                new RxSwitch()
                    .IsToggled(State.ViewMode == viewMode)
                    .OnToggled((s, e)=>SetState(_ => _.ViewMode = viewMode))
            }
            .WithHorizontalOrientation();

        public override VisualNode Render()
        {
            return new RxContentPage()
            {
                new RxStackLayout()
                {
                    RenderSwitchMode(ViewMode.TextCell),
                    RenderSwitchMode(ViewMode.ImageCell),
                    RenderSwitchMode(ViewMode.SwitchCell),
                    RenderSwitchMode(ViewMode.EntryCell),
                    RenderSwitchMode(ViewMode.ViewCell),
                    RenderCollection()
                }
            }
            .Title("ListView Test");
        }

        private VisualNode RenderCollection()
        {
            switch (State.ViewMode)
            {
                case ViewMode.TextCell:
                    return new RxTextListView<Monkey>()
                        .RenderCollection(_allMonkeys, (monkey, textCell) => RenderMonkeyWithTextCell(monkey, textCell))
                        .RowHeight(64)
                        .VFillAndExpand()
                        .HFillAndExpand();
                case ViewMode.ImageCell:
                    return new RxImageListView<Monkey>()
                        .RenderCollection(_allMonkeys, (monkey, imageCell) => RenderMonkeyWithImageCell(monkey, imageCell))
                        .RowHeight(64)
                        .VFillAndExpand()
                        .HFillAndExpand();
                case ViewMode.SwitchCell:
                    return new RxSwitchListView<Monkey>()
                        .RenderCollection(_allMonkeys, (monkey, switchCell) => RenderMonkeyWithSwitchCell(monkey, switchCell))
                        .RowHeight(48)
                        .VFillAndExpand()
                        .HFillAndExpand();
                case ViewMode.EntryCell:
                    return new RxEntryListView<Monkey>()
                        .RenderCollection(_allMonkeys, (monkey, entryCell) => RenderMonkeyWithEntryCell(monkey, entryCell))
                        .RowHeight(48)
                        .VFillAndExpand()
                        .HFillAndExpand();
                case ViewMode.ViewCell:
                    return new RxListView<Monkey>()
                        .RenderCollection(_allMonkeys, RenderMonkey)
                        .RowHeight(120)
                        .VFillAndExpand()
                        .HFillAndExpand();
            }

            return null;
        }

        private void RenderMonkeyWithTextCell(Monkey monkey, RxTextCell textCell)
        {
            textCell
                .Text(monkey.Name)
                .Detail(monkey.Details);
        }

        private void RenderMonkeyWithImageCell(Monkey monkey, RxImageCell imageCell)
        {
            imageCell
                .Text(monkey.Name)
                .Detail(monkey.Details)
                .ImageSource(monkey.ImageUrl);
        }

        private void RenderMonkeyWithSwitchCell(Monkey monkey, RxSwitchCell switchCell)
        {
            switchCell
                .Text(monkey.Name);
        }

        private void RenderMonkeyWithEntryCell(Monkey monkey, RxEntryCell entryCell)
        {
            entryCell
                .Text(monkey.Name);
        }

        private VisualNode RenderMonkey(Monkey monkey)
        {
            return new RxStackLayout()
            {
                new RxImage()
                    .Source(new Uri(monkey.ImageUrl))
                    .Margin(4),
                new RxStackLayout()
                {
                    new RxLabel(monkey.Name)
                        .FontSize(NamedSize.Default)
                        .Margin(5),
                    new RxLabel(monkey.Location)
                        .FontSize(NamedSize.Caption)
                        .Margin(5)
                }
            }
            .Padding(10)
            .WithHorizontalOrientation();
        }
    }
}
