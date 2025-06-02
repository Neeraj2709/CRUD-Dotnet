namespace BookManagementAPI.DTOs
{
    public class BookRequest
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsRead { get; set; }
    }
}