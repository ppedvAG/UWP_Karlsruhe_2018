using System;
using System.Collections.ObjectModel;
using Entities.Models;
using Entities.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntitiesTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGoogleBookSearch()
        {
            string[] searchTerms = new string[] { "Harry Potter", "Spongebob", "Herr der Ringe" };
            ObservableCollection<Book> books = null;

            foreach (var item in searchTerms)
            {
                books = GoogleBookSearch.SearchBooks(item).GetAwaiter().GetResult();
                Assert.AreEqual(10, books.Count);
            }

            books = GoogleBookSearch.SearchBooks("asdasdadasdadaa").GetAwaiter().GetResult();
            Assert.AreEqual(0, books.Count);
        }
    }
}
