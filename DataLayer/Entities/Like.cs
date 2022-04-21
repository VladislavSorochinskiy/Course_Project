namespace DataLayer.Entities
{
    public class Like
    {
        public string? UserId { get; private set; }
        public virtual User? User { get; private set; }
        public int? ItemId { get; private set; }
        public virtual Item? Item { get; private set; }

        public Like()
        {

        }
        public Like(User user, Item item)
        {
            User = user;
            UserId = user.Id;
            Item = item;
            ItemId = Item.Id;
        }
    }
}