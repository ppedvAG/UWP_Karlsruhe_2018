using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lokalisierung
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();   
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ImageSourceConverter c = new ImageSourceConverter();

            string sprache = ((ComboBox)sender).SelectedItem.ToString();
            //Änderung der Sprache
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(sprache);
            //Änderungen der Datums/Zahlenformate
            Thread.CurrentThread.CurrentCulture = new CultureInfo(sprache);
            (new MainWindow()).Show();
            this.Close();
        }
    }
}
