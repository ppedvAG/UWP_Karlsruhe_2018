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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Eigenes_UserControl
{
    public sealed partial class RadioGroup : UserControl
    {
        public RadioGroup()
        {
            this.InitializeComponent();
        }

        public event EventHandler RadioButtonChecked;

        public ICommand CheckedCommand { get; set; }

        public Dictionary<string,Color> ButtonSource
        {
            get { return (Dictionary<string,Color>)GetValue(ButtonSourceProperty); }
            set { SetValue(ButtonSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonSourceProperty =
            DependencyProperty.Register("ButtonSource", typeof(Dictionary<string,Color>), typeof(RadioGroup), new PropertyMetadata(new Dictionary<string, Color>(), ButtonSourceChanged));


        public string SelectedValue
        {
            get { return (string)GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register("SelectedValue", typeof(string), typeof(RadioGroup), new PropertyMetadata(null));



        private static void ButtonSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Auf Änderungen der Property reagieren
            if(d is RadioGroup group)
            {
                group.spRadioButtons.Children.Clear();
                if (group.ButtonSource != null)
                {
                    foreach (var item in group.ButtonSource)
                    {
                        RadioButton radioButton = new RadioButton();
                        radioButton.Content = item.Key;
                        radioButton.IsChecked = false;
                        radioButton.Foreground = new SolidColorBrush(item.Value);
                        radioButton.Checked += (sender, args) =>
                        {
                            group.RadioButtonChecked?.Invoke(group, EventArgs.Empty);
                            group.CheckedCommand.Execute(item.Key);
                            group.SelectedValue = item.Key;
                        }; 
                        group.spRadioButtons.Children.Add(radioButton);
                        
                    }   
                }
            }
        }
    }
}
