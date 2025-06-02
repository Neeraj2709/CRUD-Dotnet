namespace BookManagementAPI.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public bool IsRead { get; set; } = false;
        public int UserId { get; set; }
    }
}