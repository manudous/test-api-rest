using System;

namespace BookRestApi.Models
{
    public class BookModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public DateTime releaseDate { get; set; }
        public string author { get; set; }
    }
}
