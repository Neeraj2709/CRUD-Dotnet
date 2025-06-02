namespace BookManagementAPI.DTOs
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        
        // Add any other user properties you want to expose, e.g. CreatedDate, Roles, etc.
    }
}