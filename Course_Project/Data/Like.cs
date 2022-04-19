namespace Course_Project.Data
{
    public class Like
    {
        public int Id { get; private set; }
        public string UserId { get; private set; }
        public User User { get; private set; }

        public Like(User user)
        {
            User = user;
            UserId = user.Id;
        }
    }
}