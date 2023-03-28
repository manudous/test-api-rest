using System;

namespace BookRestApi.Models
{
    public class BookModel
    {
        public Guid id { get; set; }
        public string title { get; set; }
        public DateTime releaseDate { get; set; }
        public string author { get; set; }
    }
}
