using DevFreela.Domain.Entities;

namespace DevFreela.Domain.Repositories;

public interface IProjectRepository : IBaseRepository<ProjectEntity>
{
    Task<ProjectEntity?> GetDetailsById(Guid id);
    Task Addcomment(CommentsEntity comment);
}