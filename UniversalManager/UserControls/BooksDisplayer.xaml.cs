using Entities.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace UniversalManager.UserControls
{
    public sealed partial class BooksDisplayer : UserControl
    {
        public BooksDisplayer()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        public ObservableCollection<Book> Books
        {
            get { return (ObservableCollection<Book>)GetValue(BooksProperty); }
            set { SetValue(BooksProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Books.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BooksProperty =
            DependencyProperty.Register("Books", typeof(ObservableCollection<Book>), typeof(BooksDisplayer), new PropertyMetadata(null));



        public string ButtonContent
        {
            get { return (string)GetValue(ButtonContentProperty); }
            set { SetValue(ButtonContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonContentProperty =
            DependencyProperty.Register("ButtonContent", typeof(string), typeof(BooksDisplayer), new PropertyMetadata(null));





            
        public ICommand ButtonCommand
        {
            get { return (ICommand)GetValue(ButtonCommandProperty); }
            set { SetValue(ButtonCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonCommandProperty =
            DependencyProperty.Register("ButtonCommand", typeof(ICommand), typeof(BooksDisplayer), new PropertyMetadata(null));

        private void myControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //if(ActualWidth > 200)
            //{
            //    listbox.ItemTemplate = this.Resources["bookDetailTemplate"] as DataTemplate;
            //}
        }

        private void ScrollViewer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(sender is ScrollViewer viewer && viewer.Content is TextBlock block)
            {
                block.Width = e.NewSize.Width;
            }
        }
    }
}
