namespace DataLayer.Entities
{
    public class AdditionalItemField
    {
        public int Id { get; set; }
        public string FieldName { get; set; }
        public string Content { get; set; }
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

        public AdditionalItemField()
        {

        }
        public AdditionalItemField(string fieldName, string content)
        {
            FieldName = fieldName;
            Content = content;
        }
    }
}