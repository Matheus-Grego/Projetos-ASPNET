using DevFreela.Application.Models;
using DevFreela.Domain.Repositories;
using DevFreela.Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.DeleteSkill;

public class DeleteSkillHandler : IRequestHandler<DeleteSkillCommand, ResultViewModel>
{
    private readonly ISkillRepository _repository;

    public DeleteSkillHandler(DevFreelaDbContext context, ISkillRepository repository)
    {
        _repository = repository;
        
    }
    public async Task<ResultViewModel> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = await _repository.GetById(request.Id);

        if (skill == null)
            return ResultViewModel.Failed("skill not found");
        skill.Delete();
        await _repository.Update(skill);
        return ResultViewModel.Success();
    }
}