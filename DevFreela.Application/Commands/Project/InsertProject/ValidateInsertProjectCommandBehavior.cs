using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistance;
using MediatR;

namespace DevFreela.Application.Commands.Project.InsertProject;

public class ValidateInsertProjectCommandBehavior : IPipelineBehavior<InsertProjectCommand, ResultViewModel<Guid>>
{
    private readonly DevFreelaDbContext _dbContext;

    public ValidateInsertProjectCommandBehavior(DevFreelaDbContext context)
    {
        _dbContext = context;
    }
    
    public async Task<ResultViewModel<Guid>> Handle(InsertProjectCommand request, RequestHandlerDelegate<ResultViewModel<Guid>> next, CancellationToken cancellationToken)
    {
        var developerExists = _dbContext.Users.Any(x => x.Id == request.IdDeveloper);
        var clientExists = _dbContext.Users.Any(x => x.Id == request.IdClient);

        if (!developerExists || !clientExists)
        {
            return ResultViewModel<Guid>.Failed("Client or Developer doesn't exists.");
        }
        
        return await next();
    }
}