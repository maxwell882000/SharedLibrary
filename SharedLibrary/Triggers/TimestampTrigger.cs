using EntityFrameworkCore.Triggered;
using SharedLibrary.Attributes;

namespace SharedLibrary.Triggers
{
    public class TimestampTrigger : IBeforeSaveTrigger<ITimestamp>
    {
        public Task BeforeSave(ITriggerContext<ITimestamp> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType == ChangeType.Added || context.ChangeType == ChangeType.Modified)
            {
                context.Entity.CreatedAt = DateTime.UtcNow;
            }
            if (context.ChangeType == ChangeType.Added)
            {
                context.Entity.UpdatedAt = DateTime.UtcNow;
            }
            return Task.CompletedTask;
        }
    }
}
