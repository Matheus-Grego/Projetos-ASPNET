using DevFreela.Domain.Entities;

namespace DevFreela.Domain.Repositories;

public interface IUserRepository : IBaseRepository<UserEntity>
{
    Task InsertSkill (List<UserTechEntity> tech);
    Task<List<UserTechEntity>> GetuserBySkillId(Guid skillId);
    Task<UserEntity?> GetUserDetailsById(Guid id);
    Task<UserEntity> Login(string email, string password);
    Task<bool> ExistsByEmail(string email);
    Task<UserEntity?> GetUserByEmail(string email);

}