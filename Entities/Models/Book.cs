using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class Book : ModelBase
    { 
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetValue(ref _title, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetValue(ref _description, value);  }
        }

        private string[] _authors;
        public string[] Authors
        {
            get { return _authors; }
            set { SetValue(ref _authors, value); }
        }

        private string _isbn;
        public string ISBN
        {
            get { return _isbn; }
            set { SetValue(ref _isbn, value); }
        }

        private string _coverURL;

       

        public string CoverURL
        {
            get { return _coverURL; }
            set { SetValue(ref _coverURL, value); }
        }


        [JsonConstructor]
        public Book(string title, string description, string[] authors, string iSBN, string coverURL)
        {
            Title = title;
            Description = description;
            Authors = authors;
            ISBN = iSBN;
            CoverURL = coverURL;
        }

    }
}
