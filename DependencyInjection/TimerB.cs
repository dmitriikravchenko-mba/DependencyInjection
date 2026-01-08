using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace DependencyInjection;

public class TimerB
{
    private readonly ILogger _logger;
    private readonly IMessageStore _store;

    public TimerB(ILoggerFactory loggerFactory, IMessageStore store)
    {
        _logger = loggerFactory.CreateLogger<TimerB>();
        _store = store;
    }

    [Function("TimerB")]
    public async Task Run([TimerTrigger("0 */2 * * * *")] TimerInfo myTimer, CancellationToken token)
    {
        var message = _store.GetMessage();
        string message_sender = "TimerB";
        string message_body = $"Hello from TimerB. Time: {DateTime.Now.ToString("HH:mm:ss")}";
        if (message is not null)
        {
            _logger.LogInformation($"[TimerB] Current message: Sender: {message.MessageSender}, Body: {message.MessageBody}");
            if (message.MessageSender != "TimerB")
            {
                _logger.LogInformation($"[{message_sender}]: Setting new message.");
                await _store.SetMessage(message_sender, message_body, token);
            }
        }
        else
        {
            _logger.LogInformation("Message was null. Setting a new message.");
            await _store.SetMessage(message_sender, message_body, token);
        }
    }
}