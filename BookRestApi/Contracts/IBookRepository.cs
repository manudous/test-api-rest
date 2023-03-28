using BookRestApi.Models;
using System.Collections.Generic;

namespace BookRestApi.Contracts
{
    public interface IBookRepository
    {
        public List<BookApiModel> GetBookList(int? page, int? pageSize);
        public BookApiModel GetBook(string id);
        public void SaveBook(BookApiModel book);
        public void DeleteBook(string id);
    }
}
