namespace BookManagementAPI.DTOs
{
    public class BookResponse
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public bool IsRead { get; set; }
        public required string Username { get; set; }
    }
}