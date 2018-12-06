using System;
using System.Collections.Generic;
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
    public sealed partial class PersonDetailPage : Page
    {
        private string _zuletztGewähltesGeschlecht = string.Empty;
        private Person _zuEditierendePerson;

        public PersonDetailPage()
        {
            this.InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if(sender is RadioButton button)
            {
                _zuletztGewähltesGeschlecht = button.Content.ToString();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(e.Parameter is Person person)
            {
                tbName.Text = person.Name;
                sliderGroesse.Value = person.Größe;
                datepickerGeburt.Date = person.Geburtsdatum;
                //TODO: radio button setzen
                switchProbezeit.IsOn = person.InDerProbezeit;

                _zuEditierendePerson = person;
            }
            else
            {

            }
            base.OnNavigatedTo(e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Person.Sexes sex = _zuletztGewähltesGeschlecht != string.Empty ? 
                                (Person.Sexes)Enum.Parse(typeof(Person.Sexes), _zuletztGewähltesGeschlecht) 
                                : Person.Sexes.Male;

            string name = tbName.Text;
            bool probezeit = switchProbezeit.IsOn;
            DateTimeOffset gdatum = datepickerGeburt.Date;
            int größe = (int)sliderGroesse.Value;

            if (_zuEditierendePerson != null)
            {
                _zuEditierendePerson.Name = name;
                _zuEditierendePerson.InDerProbezeit = probezeit;
                _zuEditierendePerson.Größe = größe;
                _zuEditierendePerson.Sex = sex;
                _zuEditierendePerson.Geburtsdatum = gdatum.DateTime;
                this.Frame.Navigate(typeof(PersonCollectionPage));
            }
            else
            {
                Person neuePerson = new Person(name, gdatum.DateTime, probezeit, größe, sex);
                this.Frame.Navigate(typeof(PersonCollectionPage), neuePerson);

                //SwapValues(ref neuePerson, ref _zuEditierendePerson);
            }
        }
        
        //public static void SwapValues<T>(ref T x, ref T y)
        //{
        //    T CopyX = x;
        //    x = y;
        //    y = CopyX;
        //}
    }
}
