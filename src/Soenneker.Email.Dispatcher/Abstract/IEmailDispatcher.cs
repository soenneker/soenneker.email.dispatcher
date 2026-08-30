using Soenneker.Messages.Email;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Email.Dispatcher.Abstract;

/// <summary>
/// Defines a contract for dispatching email messages, handling routing logic
/// (e.g., queuing or direct sending) based on configuration settings.
/// </summary>
public interface IEmailDispatcher
{
    /// <summary>
    /// Dispatches the specified <paramref name="emailMessage"/> for delivery.
    /// Depending on configuration, the message may be placed on a queue
    /// or sent immediately via the underlying email sender.
    /// </summary>
    /// <param name="emailMessage">
    /// The email message to dispatch, containing recipients, subject, body, and any attachments.
    /// </param>
    /// <param name="cancellationToken">
    /// A token to observe while waiting for the dispatch operation to complete.
    /// </param>
    /// <returns>
    /// A <see cref="ValueTask"/> that represents the asynchronous dispatch operation.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when direct delivery is selected and the configured sender reports that the message was not sent.
    /// </exception>
    ValueTask Dispatch(EmailMessage emailMessage, CancellationToken cancellationToken = default);
}
