namespace BookManagementAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<Book> Books { get; set; } = new List<Book>();
    }
}