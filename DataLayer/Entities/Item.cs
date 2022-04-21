namespace DataLayer.Entities
{
    public class Item
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Tag { get; set; }
        public int CollectionId { get; private set; }
        public virtual Collection Collection { get; private set; }
        public virtual List<Like>? Likes { get; set; }
        public int? CommentId { get; set; }
        public virtual List<Comment>? Comments { get; set; }
        public virtual List<AdditionalItemField>? AdditionalItemFields { get; set; }

        public Item()
        {

        }
        public Item(string name, string tag, Collection collection)
        {
            Name = name;
            Tag = tag;
            Collection = collection;
            CollectionId = collection.Id;
        }
    }
}