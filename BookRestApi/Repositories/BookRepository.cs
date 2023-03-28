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

        public List<BookApiModel> GetBookList(int? page, int? pageSize)
        {
            try
            {
                var booksFromFile = GetBooksFromFile();
                List<BookApiModel> books = BookMappers.MapBookModelListToBookApiModelList(JsonConvert.DeserializeObject<List<BookModel>>(booksFromFile));

                if (page.HasValue && pageSize.HasValue && page.Value > 0 && pageSize.Value > 0)
                {
                    books = books.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();
                }
                return books;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving book list", ex);
            }
        }

        public BookApiModel GetBook(string bookId)
        {
            var books = GetBookList(null, null);
            var book = books.FirstOrDefault(b => b.id == bookId);
            return book;
        }

        public void AddBook(BookApiModel book)
        {
            var books = GetBookList(null, null);
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
            var books = GetBookList(null, null);
            var bookIndex = books.FindIndex(b => b.id == book.id);

            if (bookIndex < 0)
                throw new Exception("Book not found");

            books[bookIndex] = book;
            UpdateBooks(BookMappers.MapBookApiModelListToBookModelList(books));
        }

        public void SaveBook(BookApiModel book)
        {
            var books = GetBookList(null, null);
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
            var books = GetBookList(null, null);
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
