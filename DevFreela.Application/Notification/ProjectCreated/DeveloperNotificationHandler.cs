using MediatR;

namespace DevFreela.Application.Notification.ProjectCreated;

public class DeveloperNotificationHandler : INotificationHandler<ProjectCreatedNotification>
{
    public Task Handle(ProjectCreatedNotification notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Notificando os desenvolvedores da criacão do projeto {notification.Title}");
        
        return Task.CompletedTask;
    }
}