using System;

namespace BookRestApi.Models
{
    public class BookApiModel
    {
        public string id { get; set; }
        public string title { get; set; }
        public string releaseDate { get; set; }
        public string author { get; set; }
    }
}
