using BookRestApi.Models;
using System.Collections.Generic;

namespace BookRestApi.Contracts
{
    public interface IBookRepository
    {
        List<BookApiModel> GetBooks();
        BookApiModel GetBook(string id);
        void SaveBook(BookApiModel book);
        void DeleteBook(string id);
    }
}
