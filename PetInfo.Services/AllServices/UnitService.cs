using PetInfo.Repositories.Contracts.AllContracts;
using PetInfo.Services.Contracts.AllContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetInfo.Services.AllServices
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _unitRepository;

        public UnitService(IUnitRepository unitRepository)
        {
            this._unitRepository = unitRepository;
        }

        public IPetService PetService => new PetService(this._unitRepository);
        public IAnimalTypeService AnimalTypeService => new AnimalTypeService(this._unitRepository);

        public async Task<bool> SaveChangesAsync()
        {
            return await _unitRepository.SaveChangesAsync();
        }
        public void Dispose()
        {
            _unitRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            if (_unitRepository is IAsyncDisposable asyncDisposableRepo)
            {
                await asyncDisposableRepo.DisposeAsync();  // Dispose asynchronously
            }
            else
            {
                _unitRepository.Dispose();  // Fallback to synchronous disposal
            }
            GC.SuppressFinalize(this);
        }
    }
}
