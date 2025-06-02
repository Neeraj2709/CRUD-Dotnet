namespace BookManagementAPI.DTOs
{
    public class BookRequest
    {
        public required string Username { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public bool IsRead { get; set; } = false;
    }
}