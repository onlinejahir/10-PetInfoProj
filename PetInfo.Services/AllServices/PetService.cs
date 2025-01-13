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
    public class PetService : IPetService
    {
        private readonly IUnitRepository _unitRepository;
        public PetService(IUnitRepository unitRepository)
        {
            this._unitRepository = unitRepository;
        }

        public async Task AddPetAsync(Pet pet)
        {
            await _unitRepository.PetRepository.AddAsync(pet);
        }

        public IQueryable<Pet> GetAllPets()
        {
            return _unitRepository.PetRepository.GetAll();
        }

        public IQueryable<Pet> SearchPetResult(string searchString)
        {
            IQueryable<Pet> pets = _unitRepository.PetRepository.GetAll();
            pets = pets.Where(p => p.PetName.Contains(searchString));
            return pets;
        }

        public async Task<Pet?> GetPetByIdAsync(int? id)
        {
            return await _unitRepository.PetRepository.GetByIdAsync(id);
        }

        public void UpdatePet(Pet pet)
        {
            _unitRepository.PetRepository.Update(pet);
        }

        public async Task RemovePet(Pet pet)
        {
            _unitRepository.PetRepository.Remove(pet);
        }
    }
}
