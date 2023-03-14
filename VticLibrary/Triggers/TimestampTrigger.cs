using EntityFrameworkCore.Triggered;
using VitcLibrary.Attributes;

namespace VitcLibrary.Triggers
{
    public class TimestampTrigger : IBeforeSaveTrigger<Timestamp>
    {
        public Task BeforeSave(ITriggerContext<Timestamp> context, CancellationToken cancellationToken)
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
