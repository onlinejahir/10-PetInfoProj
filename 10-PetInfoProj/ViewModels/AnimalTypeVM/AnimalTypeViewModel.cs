using System.ComponentModel.DataAnnotations;

namespace _10_PetInfoProj.ViewModels.AnimalTypeVM
{
    public class AnimalTypeViewModel
    {
        [Display(Name = "ID")]
        public int AnimalTypeId { get; set; }
        [Required, StringLength(100)]
        public string? AnimalTypeName { get; set; }
    }
}
