using DevFreela.Application.Models;
using DevFreela.Domain.Repositories;
using DevFreela.Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.UpdateSkill;

public class UpdateSkillHandler : IRequestHandler<UpdateSkillCommand, ResultViewModel>
{
    private readonly ISkillRepository _repository;

    public UpdateSkillHandler(DevFreelaDbContext context, ISkillRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel> Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = await _repository.GetById(request.Id);
        if (skill == null)
            return ResultViewModel.Failed($"Skill with id {request.Id} not found.");
        
        var entity = request.ToEntity();
        skill.Update(entity);
        await _repository.Update(skill);
        
        return ResultViewModel.Success();
    }
}