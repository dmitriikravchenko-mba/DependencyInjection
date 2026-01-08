

namespace DependencyInjection
{
    public sealed class MessageStore : IMessageStore
    {
        public IntraFunctionMessageModel _message = new();
        public readonly SemaphoreSlim _gate = new(1, 1);

        public IntraFunctionMessageModel? GetMessage()
        {
            if (_message.MessageSender is null || _message.MessageBody is null)
            {
                return null;
            }
            else
            {
                return new IntraFunctionMessageModel
                {
                    MessageSender = _message.MessageSender,
                    MessageBody = _message.MessageBody
                };
            }
        }
        public async Task SetMessage(string message_sender, string message_body, CancellationToken token)
        {
            await _gate.WaitAsync(token);
            try
            {
                _message.MessageSender = message_sender;
                _message.MessageBody = message_body;
            }
            finally
            {
                _gate.Release();
            }
        }
    }
}
