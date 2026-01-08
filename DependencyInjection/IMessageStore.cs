

namespace DependencyInjection
{
    public interface IMessageStore
    {
        IntraFunctionMessageModel? GetMessage();
        Task SetMessage(string message_sender, string message_body, CancellationToken token);
    }
}
