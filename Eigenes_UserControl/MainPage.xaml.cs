using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Eigenes_UserControl
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public Dictionary<string, Color> RadioButtons { get; set; } = new Dictionary<string, Color>();

        //public ViewModel Model { get; set; }

        public ICommand MyProperty { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            RadioButtons.Add("Option1", Colors.Black);
            RadioButtons.Add("Option2", Colors.Blue);
            RadioButtons.Add("Option3", Colors.Red);
            //Model = new ViewModel();
            //this.DataContext = Model;
            
        }

        private void radio1_RadioButtonChecked(object sender, EventArgs e)
        {
            //...
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //if(e.Parameter is ViewModel model)
            //{
            //    Model = model;
            //}
            base.OnNavigatedTo(e);
        }
    }
}
