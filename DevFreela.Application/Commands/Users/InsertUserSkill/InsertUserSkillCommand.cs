using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.Users.InsertUserSkill;

public class InsertUserSkillCommand : IRequest<ResultViewModel>
{
    public InsertUserSkillCommand() { }

    public InsertUserSkillCommand(Guid userId, Guid[] tech)
    {
        UserId = userId;
        TechnologiesID = tech;
    }
    public Guid UserId { get; set; }
    public Guid[] TechnologiesID { get; set; }
}