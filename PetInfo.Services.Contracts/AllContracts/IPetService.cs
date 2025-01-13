using PetInfo.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetInfo.Services.Contracts.AllContracts
{
    public interface IPetService
    {
        Task AddPetAsync(Pet pet);
        IQueryable<Pet> GetAllPets();
        IQueryable<Pet> SearchPetResult(string searchString);
        Task<Pet?> GetPetByIdAsync(int? id);
        void UpdatePet(Pet pet);
        Task RemovePet(Pet pet);
    }
}
