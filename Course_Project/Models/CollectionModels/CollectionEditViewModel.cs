using DataLayer.Entities;
using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models.CollectionModels
{
    public class CollectionEditViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Topic { get; set; }

        [Required]
        public string Description { get; set; }

        public string? Image { get; set; }
    }
}
