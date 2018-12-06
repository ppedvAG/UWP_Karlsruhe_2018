using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Hello_World
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

       
        private void Hello_World_Button_Click(object sender, RoutedEventArgs e)
        {
            Button neuerButton = new Button()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                FontSize = 20,
                Background = new SolidColorBrush(Colors.DeepPink)
            };

            neuerButton.Click += (sen, arg) =>
            {
                new MessageDialog("Hello World!").ShowAsync();
            };

            //mainGrid.Children.Add(neuerButton);

            if(VisualTreeHelper.GetParent(button1) is Panel panel)
            {
                panel.Children.Add(neuerButton);
            }
        }

        private void NeuerButton_Click(object sender, RoutedEventArgs e)
        {
            new MessageDialog("Hello World!").ShowAsync();
        }
    }
}
