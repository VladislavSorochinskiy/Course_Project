namespace DataLayer.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public int CollectionId { get; set; }
        public virtual Collection Collection { get; set; }
        public virtual List<AdditionalItemField> AdditionalItemFields { get; set; }

        public Item()
        {

        }
        public Item(string name, string tag)
        {
            Name = name;
            Tag = tag;
        }
    }
}