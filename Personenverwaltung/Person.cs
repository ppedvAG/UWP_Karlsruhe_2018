using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personenverwaltung
{
    public enum Geschlechter { Männlich, Weiblich } 
 
    public class Person
    {
        public enum Sexes
        {
            Male, Female, Transgender, Undefined, Unclear, Unknown 
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private DateTime _geburtsdatum;

        public DateTime Geburtsdatum
        {
            get { return _geburtsdatum; }
            set { _geburtsdatum = value; }
        }

        private bool _inDerProbezeit;

        public bool InDerProbezeit
        {
            get { return _inDerProbezeit; }
            set { _inDerProbezeit = value; }
        }

        private int _größe;
        public int Größe
        {
            get { return _größe; }
            set { _größe = value; }
        }

        private Sexes _sex;

        

        public Sexes Sex
        {
            get { return _sex; }
            set { _sex = value; }
        }

        public Person(string name, DateTime geburtsdatum, bool inDerProbezeit, int größe, Sexes sex)
        {
            Name = name;
            Geburtsdatum = geburtsdatum;
            InDerProbezeit = inDerProbezeit;
            Größe = größe;
            Sex = sex;
        }

        public override string ToString()
        {
            return $"{Name} ({Sex}), (geb. {Geburtsdatum.ToShortDateString()}), {Größe} cm";
        }
    }
}
