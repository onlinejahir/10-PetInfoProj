using Microsoft.EntityFrameworkCore;
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
    public class PetRepository : GenericRepository<Pet>, IPetRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public PetRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }
        public override async Task<Pet?> GetByIdAsync(int? id)
        {
            return await _dbContext.Pets
                .Include(p => p.AnimalType)
                .Where(p => p.PetId == id)
                .FirstOrDefaultAsync();
        }
        public override IQueryable<Pet> GetAll()
        {
            return _dbContext.Pets
                .Include(p => p.AnimalType).AsQueryable();
        }
    }
}
