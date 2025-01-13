using PetInfo.Models.EntityModels;
using PetInfo.Repositories.Contracts.AllContracts;
using PetInfo.Services.Contracts.AllContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetInfo.Services.AllServices
{
    public class AnimalTypeService : IAnimalTypeService
    {
        private readonly IUnitRepository _unitRepository;
        public AnimalTypeService(IUnitRepository unitRepository)
        {
            this._unitRepository = unitRepository;
        }

        public async Task AddAnimalTypeAsync(AnimalType animalType)
        {
            await _unitRepository.AnimalTypeRepository.AddAsync(animalType);
        }

        public async Task<IEnumerable<AnimalType>> GetAllAnimalTypeAsync()
        {
            return _unitRepository.AnimalTypeRepository.GetAll();
        }

        public async Task<AnimalType?> GetAnimalTypeByIdAsync(int id)
        {
            return await _unitRepository.AnimalTypeRepository.GetByIdAsync(id);
        }

        public void RemoveAnimalType(AnimalType existingAnimalType)
        {
            _unitRepository.AnimalTypeRepository.Remove(existingAnimalType);
        }

        public void UpdateAnimalType(AnimalType existingAnimalType)
        {
            _unitRepository.AnimalTypeRepository.Update(existingAnimalType);
        }
    }
}
