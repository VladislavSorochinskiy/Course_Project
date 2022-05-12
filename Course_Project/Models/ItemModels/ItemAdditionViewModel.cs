using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models.ItemModels
{
    public class ItemAdditionViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Tag { get; set; }
        public List<string>? FieldNames { get; set; }
        [Required]
        public List<string>? Contents { get; set; }
    }
}