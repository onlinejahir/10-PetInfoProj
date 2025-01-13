using PetInfo.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetInfo.Services.Contracts.AllContracts
{
    public interface IAnimalTypeService
    {
        Task AddAnimalTypeAsync(AnimalType animalType);
        Task<IEnumerable<AnimalType>> GetAllAnimalTypeAsync();
        Task<AnimalType?> GetAnimalTypeByIdAsync(int id);
        void RemoveAnimalType(AnimalType existingAnimalType);
        void UpdateAnimalType(AnimalType existingAnimalType);
    }
}
