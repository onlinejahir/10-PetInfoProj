using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetInfo.Models.EntityModels
{
    public class Pet
    {
        [Key]
        public int PetId { get; set; }
        [Required]
        [StringLength(100)]
        public string? PetName { get; set; }
        [Required]
        public int Age { get; set; }
        [Required, StringLength(50)]
        public string Color { get; set; } = string.Empty;
        [Required, StringLength(20)]
        public string? Gender { get; set; }
        [Required, StringLength(100)]
        public string OwnerName { get; set; } = string.Empty;
        [Required, StringLength(200)]
        public string? Address { get; set; }
        [StringLength(100)]
        public string ImageName { get; set; } = string.Empty;
        //Relationship with AnimalType
        [ForeignKey("AnimalType")]
        public int AnimalTypeId { get; set; }  //Foreign key
        public AnimalType AnimalType { get; set; }  //Reference navigation property
    }
}
