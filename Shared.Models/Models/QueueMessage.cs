namespace Shared.Models.Models
{
    public enum QueueMessageType
    {
        Insert,
        Delete
    }

    public class QueueMessage<T>
    {
        public T? Data { get; set; }

        public QueueMessageType Type { get; set; }
    }
}
