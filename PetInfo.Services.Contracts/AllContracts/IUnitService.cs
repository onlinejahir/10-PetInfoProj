using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetInfo.Services.Contracts.AllContracts
{
    public interface IUnitService : IDisposable, IAsyncDisposable
    {
        public IPetService PetService { get; }
        public IAnimalTypeService AnimalTypeService { get; }
        Task<bool> SaveChangesAsync();
    }
}
