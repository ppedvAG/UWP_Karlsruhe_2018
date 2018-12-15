using Entities.Helper;
using Entities.Models;
using Entities.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Entities.ViewModels
{
    public class BookSearchViewModel : ModelBase
    {
        //Events
        private EventHandler _searchTriggered;
        public event EventHandler SearchTriggered
        {
            add
            {
                if(_searchTriggered == null || !_searchTriggered.GetInvocationList().ToList().Contains(value))
                {
                    _searchTriggered += value;
                }
            }
            remove
            {
                _searchTriggered -= value;
            }
        }


        
        
        //Properties
        private string _searchTerm;

        public string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                SetValue(ref _searchTerm, value);
                (SearchCommand as DelegateCommand)?.OnCanExecuteChanged();
            }
        }

        private ObservableCollection<Book> _books;

        public ObservableCollection<Book> Books
        {
            get { return _books; }
            set
            {
                SetValue(ref _books, value);
            }
        }

        public ICommand SearchCommand { get; set; }
        public ICommand BookSelectedCommand { get; set; }

        public BookSearchViewModel()
        {
            SearchCommand = new DelegateCommand(async p =>
            {
                //Buchsuche
                _searchTriggered?.Invoke(this, EventArgs.Empty);
                Books = await GoogleBookSearch.SearchBooks(SearchTerm);
            },
            p =>
                //Wann kann die Suche angestoßen werden?
                !string.IsNullOrWhiteSpace(SearchTerm)
            );

            BookSelectedCommand = new DelegateCommand(p =>
            {
                if (p is Book book)
                {
                    //TODO: ViewModel bauen
                    TodoViewModel model = new TodoViewModel(book);
                    NavigationHelper.Service.Navigate(Interfaces.NavigationTarget.TodoItems, model);
                }
            });
        }
    }
}
