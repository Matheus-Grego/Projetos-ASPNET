using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Queries.GetUserBySkill;

public class GetUsersBySkillQuery : IRequest<ResultViewModel<List<UserModel>>>
{
    public GetUsersBySkillQuery(Guid skillId)
    {
        SkillId = skillId;
    }
    public Guid SkillId { get; }
}