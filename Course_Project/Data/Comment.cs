namespace Course_Project.Data
{
    public class Comment
    {
        public int Id { get; private set; }
        public string Description { get; set; }
        public string UserId { get; private set; }
        public User User { get; private set; }

        public Comment(string description, User user)
        {
            UserId = user.Id;
            Description = description;
            User = user;
        }
    }
}
