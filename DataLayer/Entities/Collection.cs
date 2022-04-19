namespace Course_Project.DataLayer
{
    public class Collection
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public string UserId { get; private set; }
        public User User { get; private set; }

        public Collection(User user, string name, string topic, string description) : base()
        {
            User = user;
            UserId = user.Id;
            Name = name;
            Topic = topic;
            Description = description;
        }
    }
}
