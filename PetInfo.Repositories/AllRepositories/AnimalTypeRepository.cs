using PetInfo.Database.Data;
using PetInfo.Models.EntityModels;
using PetInfo.Repositories.Contracts.AllContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetInfo.Repositories.AllRepositories
{
    public class AnimalTypeRepository : GenericRepository<AnimalType>, IAnimalTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public AnimalTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }
    }
}
