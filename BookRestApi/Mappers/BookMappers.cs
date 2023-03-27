using BookRestApi.Models;
using System;
using System.Collections.Generic;

namespace BookRestApi.Mappers
{
    public class BookMappers
    {
        public static BookModel MapBookApiModelToBookModel(BookApiModel bookApiModel)
        {
            return new BookModel
            {
                id = int.Parse(bookApiModel.id),
                title = bookApiModel.title,
                releaseDate = DateTime.Parse(bookApiModel.releaseDate),
                author = bookApiModel.author
            };
        }

        public static BookApiModel MapBookModelToBookApiModel(BookModel bookModel)
        {
            return new BookApiModel
            {
                id = bookModel.id.ToString(),
                title = bookModel.title,
                releaseDate = bookModel.releaseDate.ToShortDateString(),
                author = bookModel.author
            };
        }

        public static List<BookModel> MapBookApiModelListToBookModelList(List<BookApiModel> bookApiModelList)
        {
            List<BookModel> bookModelList = new();

            foreach (var bookApiModel in bookApiModelList)
            {
                bookModelList.Add(MapBookApiModelToBookModel(bookApiModel));
            }

            return bookModelList;
        }

        public static List<BookApiModel> MapBookModelListToBookApiModelList(List<BookModel> bookModelList)
        {
            List<BookApiModel> bookApiModelList = new();

            foreach (var bookModel in bookModelList)
            {
                bookApiModelList.Add(MapBookModelToBookApiModel(bookModel));
            }

            return bookApiModelList;
        }
    }
}
