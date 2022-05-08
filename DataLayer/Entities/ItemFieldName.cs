namespace DataLayer.Entities
{
    public class ItemFieldName
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CollectionId { get; set; }
        public virtual Collection Collection { get; set; }

        public ItemFieldName()
        {

        }
        public ItemFieldName(string Name, Collection Collection)
        {
            this.Name = Name;
            this.Collection = Collection;
            CollectionId = Collection.Id;
        }
    }
}
