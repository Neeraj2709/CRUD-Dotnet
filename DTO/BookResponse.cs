namespace BookManagementAPI.DTOs
{
    public class BookResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public bool IsRead { get; set; }
        public int UserId { get; set; }
    }
}