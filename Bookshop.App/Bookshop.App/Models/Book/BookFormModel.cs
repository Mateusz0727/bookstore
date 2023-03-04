namespace Bookshop.App.Models.Book
{
    public class BookFormModel
    {
        public long Id { get; set; }

        public string Title { get; set; } = null!; 

        public string Autor { get; set; } = null!;

        public string Describe { get; set; } = null!;

        public double Price { get; set; }

        public string PublishingHouse { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;

    }
}
