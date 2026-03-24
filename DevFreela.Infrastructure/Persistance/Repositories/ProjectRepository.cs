using DevFreela.Domain.Entities;
using DevFreela.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistance.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly DevFreelaDbContext _dbContext;

    public ProjectRepository(DevFreelaDbContext context)
    {
        _dbContext = context;
    }
    
    public async Task<List<ProjectEntity?>> GetAll(string search = "", int? page = 1, int? pageSize = 10)
    {
        var projects = await _dbContext.Projects
            .Include(p => p.Client)
            .Include(p => p.Developer)
            .Include(p => p.Technologies) 
            .Where(p => !p.IsDeleted && (search == "" || p.Title.ToLower().Contains(search.ToLower()) ||
                                         p.Description.ToLower().Contains(search.ToLower())))
            .Skip((page.Value - 1) * pageSize.Value)
            .Take(pageSize.Value)
            .ToListAsync();
        
        return projects;
    }

    public async Task<ProjectEntity?> GetDetailsById(Guid id)
    {
        var project = await _dbContext.Projects
            .Include(p => p.Client)
            .Include(p => p.Developer)
            .SingleOrDefaultAsync(p => p.Id == id);
        
        return project;
    }
    
    public async Task<ProjectEntity?> GetById(Guid id)
    {
        var project = await _dbContext.Projects
            .SingleOrDefaultAsync(p => p.Id == id);
        
        return project;
    }

    public async Task<Guid> Add(ProjectEntity project)
    {
        await _dbContext.Projects.AddAsync(project);
        await _dbContext.SaveChangesAsync();
        return project.Id;
    }

    public async Task Update(ProjectEntity project)
    {
        _dbContext.Projects.Update(project);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Addcomment(CommentsEntity comment)
    {
        await _dbContext.Comments.AddAsync(comment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> Exists(Guid id)
    {
        return await _dbContext.Projects.AnyAsync(p => p.Id == id);
    }
}