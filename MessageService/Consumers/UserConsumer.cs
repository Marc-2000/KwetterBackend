using MassTransit;
using MessageService.BLL.Repositories;
using Shared.Models.Models;

namespace MessageService.Consumers
{
    public class UserConsumer : IConsumer<QueueMessage<SharedUser>>
    {
        private readonly IUserRepository _userRepository;

        public UserConsumer(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task Consume(ConsumeContext<QueueMessage<SharedUser>> context)
        {
            try
            {
                switch (context.Message.Type)
                {
                    case QueueMessageType.Insert:
                        await _userRepository.RegisterUser(context.Message.Data);
                        break;

                    case QueueMessageType.Delete:
                        await _userRepository.DeleteUser(context.Message.Data);
                        break;
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.ToString());
            }
        }
    }
}