using DevFreela.Domain.Entities;
using DevFreela.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistance.Repositories;

public class SkillRepository : ISkillRepository
{
    private readonly DevFreelaDbContext _dbContext;

    public SkillRepository(DevFreelaDbContext context)
    {
        _dbContext = context;
    }
    public async Task<List<TechEntity>> GetAll(string search, int? page, int? pageSize)
    {
        return await _dbContext.Techs.ToListAsync();
    }

    public Task<TechEntity?> GetById(Guid id)
    {
        return _dbContext.Techs.SingleOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Guid> Add(TechEntity entity)
    {
        await _dbContext.Techs.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task Update(TechEntity entity)
    {
        _dbContext.Techs.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> Exists(Guid id)
    {
        return await _dbContext.Techs.AnyAsync(x => x.Id == id);
    }
}