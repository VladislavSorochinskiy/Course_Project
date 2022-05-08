namespace DataLayer.Entities
{
    public class Item
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public string? Tag { get; set; }
        public virtual List<Like>? Likes { get; set; }
        public virtual List<Comment>? Comments { get; set; }
        public int CollectionId { get; set; }
        public virtual Collection Collection { get; set; }
        public virtual List<AdditionalItemField> AdditionalItemFields { get; set; }

        public Item()
        {
            
        }
        public Item(string Name, string Tag, Collection Collection)
        {
            this.Name = Name;
            this.Tag = Tag;
            this.Collection = Collection;
            CollectionId = Collection.Id;   
        }
    }
}