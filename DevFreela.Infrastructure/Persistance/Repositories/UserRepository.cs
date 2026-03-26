using DevFreela.Domain.Entities;
using DevFreela.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistance.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DevFreelaDbContext _dbContext;

    public UserRepository(DevFreelaDbContext context)
    {
        _dbContext = context;
    }
    public async Task<List<UserEntity>> GetAll(string search, int? page, int? pageSize)
    {
        return await _dbContext.Users.Include(x => x.Technologies).Include(x => x.ProjectsAsDeveloper).Where(x => x.Username.ToLower().Contains(search.ToLower()))
            .Skip((page.Value - 1) * pageSize.Value)
            .Take(pageSize.Value).ToListAsync();
    }

    public async Task<UserEntity?> GetById(Guid id)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);
    }

    public async Task<UserEntity?> GetUserDetailsById(Guid id)
    {
        return await _dbContext.Users
            .Include(t => t.Technologies)
            .ThenInclude(x => x.Technology)
            .SingleOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Guid> Add(UserEntity entity)
    {
        await _dbContext.Users.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        
        return entity.Id;
    }

    public async Task Update(UserEntity entity)
    {
         _dbContext.Users.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> Exists(Guid id)
    {
        return  await _dbContext.Users.AnyAsync(u => u.Id == id);
    }

    public async Task InsertSkill(List<UserTechEntity> tech)
    {
        await _dbContext.UserTechs.AddRangeAsync(tech);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<UserTechEntity>> GetuserBySkillId(Guid skillId)
    {
        return await _dbContext.UserTechs.Where(x => x.TechId == skillId).ToListAsync();

    }

    public async Task<UserEntity> Login(string email, string password)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(x => x.Email == email && x.Password == password);
    }
}