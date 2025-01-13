using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetInfo.Models.EntityModels
{
    public class AnimalType
    {
        public int AnimalTypeId { get; set; }
        [Required, StringLength(100)]
        public string? AnimalTypeName { get; set; }

        //Relationship with Pet
        public ICollection<Pet> Pets { get; set; } = new List<Pet>();  //Collection navigation property
    }
}
