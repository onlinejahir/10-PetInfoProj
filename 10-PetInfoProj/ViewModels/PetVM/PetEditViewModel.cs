using PetInfo.Models.EntityModels;
using System.ComponentModel.DataAnnotations;

namespace _10_PetInfoProj.ViewModels.PetVM
{
    public class PetEditViewModel
    {
        [Display(Name = "ID")]
        public int PetId { get; set; }
        [Required]
        [StringLength(100), Display(Name = "Pet Name")]
        public string? PetName { get; set; }
        [Required]
        public int Age { get; set; }
        [Required, StringLength(50)]
        public string Color { get; set; } = string.Empty;
        [Required, StringLength(50)]
        public string? Gender { get; set; }
        [Required, StringLength(100)]
        [Display(Name = "Owner Name")]
        public string OwnerName { get; set; } = string.Empty;
        [Required, StringLength(200)]
        public string? Address { get; set; }
        [StringLength(100)]
        public string ImageName { get; set; } = string.Empty;
        [Display(Name = "Upload Image")]
        //[Required(ErrorMessage = "Please upload the pet image")]
        //[FileExtensions(Extensions = "jpg,jpeg,png", ErrorMessage = "Only image files (JPG, JPEG, PNG) are allowed.")]
        public IFormFile? ImageFile { get; set; }
        [Required]
        [Display(Name = "Animal type")]
        public int AnimalTypeId { get; set; }
        public AnimalType? AnimalType { get; set; }
    }
}
