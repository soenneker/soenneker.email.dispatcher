using Microsoft.Extensions.Configuration;
using Soenneker.Email.Dispatcher.Abstract;
using Soenneker.Email.Senders.Abstract;
using Soenneker.Email.Util.Abstract;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Extensions.Configuration;
using Soenneker.Messages.Email;
using Soenneker.Extensions.ValueTask;
using Soenneker.Extensions.Task;

namespace Soenneker.Email.Dispatcher;

/// <inheritdoc cref="IEmailDispatcher"/>
public sealed class EmailDispatcher: IEmailDispatcher
{
    private readonly IEmailUtil _emailUtil;
    private readonly IEmailSender _sender;

    private readonly bool _useQueue;

    public EmailDispatcher(IEmailUtil emailUtil, IConfiguration configuration, IEmailSender sender)
    {
        _emailUtil = emailUtil;
        _sender = sender;

        _useQueue = configuration.GetValueStrict<bool>("Email:UseQueue");
    }

    public async ValueTask Dispatch(EmailMessage emailMessage, CancellationToken cancellationToken = default)
    {
        if (_useQueue)
        {
            await _emailUtil.PlaceOnQueue(emailMessage, cancellationToken).NoSync();
        }
        else
        {
            await _sender.Send(emailMessage, cancellationToken).NoSync();
        }
    }
}
