using BookRestApi.Contracts;
using BookRestApi.Mappers;
using BookRestApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookRestApi.Repositories
{
    public class BookRepository : IBookRepository
    {
        const string JSON_PATH = @"C:\Users\manud\Source\Repos\test-api-rest\BookRestApi\Resources\Books.json";

        public List<BookApiModel> GetBooks()
        {
            try
            {
                var booksFromFile = GetBooksFromFile();
                List<BookApiModel> books = BookMappers.MapBookModelListToBookApiModelList(JsonConvert.DeserializeObject<List<BookModel>>(booksFromFile));
                return books;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public BookApiModel GetBook(string id)
        {
            try
            {
                var books = GetBooks();
                var book = books.FirstOrDefault(b => b.id == id);
                return book;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddBook(BookApiModel book)
        {
            var books = GetBooks();
            var existBook = books.Exists(b => b.id == book.id);

            if (existBook)
            {
                throw new Exception("There is already a book with this Id");
            }
            books.Add(book);
            UpdateBooks(BookMappers.MapBookApiModelListToBookModelList(books));
        }

        public void UpdateBook(BookApiModel book)
        {
            var books = GetBooks();
            var bookIndex = books.FindIndex(b => b.id == book.id);

            if (bookIndex < 0)
                throw new Exception("Book not found");

            books[bookIndex] = book;
            UpdateBooks(BookMappers.MapBookApiModelListToBookModelList(books));
        }

        public void SaveBook(BookApiModel book)
        {
            var books = GetBooks();
            var bookIndex = books.FindIndex(b => b.id == book.id);

            if (bookIndex < 0)
            {
                AddBook(book);
            }
            else
            {
                UpdateBook(book);
            }
        }

        public void DeleteBook(string id)
        {
            var books = GetBooks();
            var bookIndex = books.FindIndex(b => b.id == id);

            if (bookIndex < 0)
                throw new Exception("Book not found");

            books.RemoveAt(bookIndex);
            UpdateBooks(BookMappers.MapBookApiModelListToBookModelList(books));
        }

        private static string GetBooksFromFile()
        {
            return File.ReadAllText(JSON_PATH);
        }

        private static void UpdateBooks(List<BookModel> books)
        {
            var booksJson = JsonConvert.SerializeObject(books, Formatting.Indented);
            File.WriteAllText(JSON_PATH, booksJson);
        }
    }
}
