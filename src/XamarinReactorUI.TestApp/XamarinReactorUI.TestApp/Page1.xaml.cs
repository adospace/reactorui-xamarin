using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinReactorUI.TestApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();

            System.Diagnostics.Debug.Assert(lblTest.TextColor == Color.Red);

            lblTest.TextColor = Color.Green;

            System.Diagnostics.Debug.Assert(lblTest.TextColor == Color.Green);

            lblTest.ClearValue(Label.TextColorProperty);

            System.Diagnostics.Debug.Assert(lblTest.TextColor == Color.Red);


        }
    }

    public class Page1ViewModel : BindableObject
    {
        private string _text = "Page1ViewModel";
        public string Text
        {
            get => _text;
            set
            {
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}