using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.Skills.DeleteSkill;

public class DeleteSkillCommand : IRequest<ResultViewModel>
{
    public DeleteSkillCommand(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; private set; }
}