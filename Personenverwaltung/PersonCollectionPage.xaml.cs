using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Personenverwaltung
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PersonCollectionPage : Page
    {

        private static ObservableCollection<Person> _personenListe;

        public static  ObservableCollection<Person> PersonenListe
        {
            get
            {
                if (_personenListe == null)
                {
                    _personenListe = new ObservableCollection<Person>();
                }
                return _personenListe;
            }
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is Person person)
            {
                PersonenListe.Add(person);
            }
            
            base.OnNavigatedTo(e);
        }

        public PersonCollectionPage()
        {
            this.InitializeComponent();
            listboxPersonen.ItemsSource = PersonenListe;
        }

        private void listboxPersonen_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PersonDetailPage), listboxPersonen.SelectedItem);
        }
    }
}
