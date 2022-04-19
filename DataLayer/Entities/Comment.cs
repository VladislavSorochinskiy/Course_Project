namespace Course_Project.DataLayer
{
    public class Comment
    {
        public int Id { get; private set; }
        public string Description { get; set; }
        public string UserId { get; private set; }
        public virtual User User { get; private set; }
        public virtual List<Like>? Likes { get; set; }

        public Comment(string description, User user)
        {
            UserId = user.Id;
            Description = description;
            User = user;
        }
    }
}
