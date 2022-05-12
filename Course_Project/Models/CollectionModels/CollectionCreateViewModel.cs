using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models.CollectionModels
{
    public class CollectionCreateViewModel
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Topic { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        //public string? Image { get; set; }
    }
}