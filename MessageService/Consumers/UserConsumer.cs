using MassTransit;
using Shared.Models.Models;

namespace MessageService.Consumers
{
    public class UserConsumer : IConsumer<SharedUser>
    {
        public async Task Consume(ConsumeContext<SharedUser> context)
        {
            await Task.Run(() => { var obj = context.Message; });
        }
    }
}