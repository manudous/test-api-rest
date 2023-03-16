using BookRestApi.Contracts;
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
        const string JSON_PATH = @"D:\Curro\BookRestApi\BookRestApi\Resources\Books.json";

        public List<Book> GetBooks()
        {
            try
            {
                var booksFromFile = GetBooksFromFile();
                List<Book> books = JsonConvert.DeserializeObject<List<Book>>(booksFromFile);
                return books;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Book GetBook(int id)
        {
            try
            {
                var books = GetBooks();
                var book = books.FirstOrDefault(b => b.Id == id);
                return book;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void AddBook(Book book)
        {
            var books = GetBooks();
            var existBook = books.Exists(b => b.Id == book.Id);

            if (existBook)
            {
                throw new Exception("There is already a book with this Id");
            }
            books.Add(book);
            UpdateBooks(books);
        }

        public void SaveBook(Book book, int id)
        {
            var books = GetBooks();
            var existBook = books.Exists(b => b.Id == book.Id);
            if (existBook)
            {

                if (existBook)
                {
                    throw new Exception("There is already a book with this Id");
                }
                books.Add(book);
                UpdateBooks(books);
            }
        }

        public void UpdateBook(Book book)
        {
            var books = GetBooks();
            var bookIndex = books.FindIndex(b => b.Id == book.Id);

            if (bookIndex < 0)
                throw new Exception("Book not found");

            books[bookIndex] = book;
            UpdateBooks(books);
        }

        public void DeleteBook(int id)
        {
            var books = GetBooks();
            var bookIndex = books.FindIndex(b => b.Id == id);

            if (bookIndex < 0)
                throw new Exception("Book not found");

            books.RemoveAt(bookIndex);
            UpdateBooks(books);
        }

        private static string GetBooksFromFile()
        {
            return File.ReadAllText(JSON_PATH);
        }

        private static void UpdateBooks(List<Book> books)
        {
            var booksJson = JsonConvert.SerializeObject(books, Formatting.Indented);
            File.WriteAllText(JSON_PATH, booksJson);
        }
    }
}
