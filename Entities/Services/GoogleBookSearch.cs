using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Newtonsoft.Json;

namespace Entities.Services
{
    public class GoogleBookSearch
    {
        public async static Task<ObservableCollection<Book>> SearchBooks(string searchTerm)
        {
            HttpClient client = new HttpClient();
            try
            {

                string jsonString = await client.GetStringAsync($"https://www.googleapis.com/books/v1/volumes?q={searchTerm.Replace(' ','+')}");
                BookSearchResult searchResult = JsonConvert.DeserializeObject<BookSearchResult>(jsonString);

                ObservableCollection<Book> books = new ObservableCollection<Book>();

                foreach (var item in searchResult.items)
                {
                    string isbn = null;
                    if (item.volumeInfo?.industryIdentifiers != null && item.volumeInfo?.industryIdentifiers.Length > 0)
                    {
                        isbn = item.volumeInfo?.industryIdentifiers?[0].identifier;
                    }

                    Book book = new Book(   item.volumeInfo?.title,
                                            item.volumeInfo?.description,
                                            item.volumeInfo?.authors,
                                            isbn,
                                            item.volumeInfo?.imageLinks?.smallThumbnail);
                    books.Add(book);
                }

                return books;
            }
            catch (Exception)
            {
                return new ObservableCollection<Book>();
            }
        }
    }
}
