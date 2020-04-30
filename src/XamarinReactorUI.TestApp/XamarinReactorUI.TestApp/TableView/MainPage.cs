using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinReactorUI.TestApp.TableView
{
    public class MainPage : RxComponent
    {
        public override VisualNode Render()
        {
            return new RxNavigationPage()
            {
                new RxContentPage()
                { 
                    new RxTableView()
                    { 
                        new RxTableRoot()
                        {
                            new RxTableSection()
                            {
                                new RxViewCell()
                                {
                                    new RxLabel("Settings under section 1")
                                },
                                new RxViewCell()
                                {
                                    new RxLabel("Other settings under section 1")
                                }
                            }
                            .Title("Section 1"),
                            new RxTableSection()
                            {
                                new RxSwitchCell()
                                    .Text("Entry cell"),
                                new RxViewCell()
                                {
                                    new RxLabel("Other settings under section 2")
                                },
                                new RxEntryCell()
                                    .Label("Switch cell"),
                            }
                            .Title("Section 2")
                        }
                    }
                    .Intent(Xamarin.Forms.TableIntent.Settings)                    
                }
                .Title("Settings")
            };
        }
    }
}
