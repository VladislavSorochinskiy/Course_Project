namespace DataLayer.Entities
{
    public class Comment
    {
        public int Id { get; private set; }
        public string Description { get; set; }
        public string UserId { get; private set; }
        public virtual User User { get; private set; }
        public int? ItemId { get; private set; }
        public virtual Item? Item { get; private set; }

        public Comment()
        {

        }
        public Comment(string description, User user, Item item)
        {
            UserId = user.Id;
            Description = description;
            User = user;
            Item = item;
            ItemId = item.Id;
        }
    }
}
