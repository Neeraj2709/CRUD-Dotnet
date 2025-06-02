namespace BookManagementAPI.Models
{
    public class Book
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public bool IsRead { get; set; } = false;
        public int UserId { get; set; }
    }
}