using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace DependencyInjection;

public class TimerA
{
    private readonly ILogger _logger;
    private readonly IMessageStore _store;

    public TimerA(ILoggerFactory loggerFactory, IMessageStore store)
    {
        _logger = loggerFactory.CreateLogger<TimerA>();
        _store = store;
    }

    [Function("TimerA")]
    public async Task Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, CancellationToken token)
    {
        //Executes every minute
        var message = _store.GetMessage();
        string message_sender = "TimerA";
        string message_body = $"Hello from TimerA. Time: {DateTime.Now.ToString("HH:mm:ss")}";
        if (message is not null)
        {
            _logger.LogInformation($"[TimerA] Current message: Sender: {message.MessageSender}, Body: {message.MessageBody}");
            if(message.MessageSender != "TimerA")
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