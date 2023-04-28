using EntityFrameworkCore.Triggered;
using Microsoft.AspNetCore.Http;
using SharedLibrary.Attributes;

namespace SharedLibrary.Triggers;


public class OwnershipTrigger : IBeforeSaveTrigger<IOwnership>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public OwnershipTrigger(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public Task BeforeSave(ITriggerContext<IOwnership> context, CancellationToken cancellationToken)
    {
        var Id = long.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
        if (context.ChangeType == ChangeType.Added)
        {
            context.Entity.CreatedBy = Id;
        }
        if (context.ChangeType == ChangeType.Added || context.ChangeType == ChangeType.Modified)
        {
            context.Entity.UpdatedBy = Id;
        }
        return Task.CompletedTask;
    }
}
