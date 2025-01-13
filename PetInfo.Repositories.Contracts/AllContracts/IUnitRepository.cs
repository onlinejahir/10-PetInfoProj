using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetInfo.Repositories.Contracts.AllContracts
{
    public interface IUnitRepository : IDisposable, IAsyncDisposable
    {
        IPetRepository PetRepository { get; }
        IAnimalTypeRepository AnimalTypeRepository { get; }
        Task<bool> SaveChangesAsync();
    }
}
