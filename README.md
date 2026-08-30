[![](https://img.shields.io/nuget/v/soenneker.email.dispatcher.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.email.dispatcher/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.email.dispatcher/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.email.dispatcher/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.email.dispatcher.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.email.dispatcher/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.email.dispatcher/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.email.dispatcher/actions/workflows/codeql.yml)

# Soenneker.Email.Dispatcher

Routes an `EmailMessage` either to Azure Service Bus or directly to an `IEmailSender`, based on the `Email:UseQueue` configuration value.

## Install

```bash
dotnet add package Soenneker.Email.Dispatcher
```

## Configure routing

```json
{
  "Email": {
    "UseQueue": false
  }
}
```

The value is required and is read when `EmailDispatcher` is constructed. Changing configuration afterward does not change an existing dispatcher's route.

Register an `IEmailSender`, then choose the dispatcher lifetime that matches the consuming application:

```csharp
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Email.Dispatcher.Abstract;
using Soenneker.Email.Dispatcher.Registrars;
using Soenneker.Email.Senders.Abstract;

services.AddSingleton<IEmailSender, AppEmailSender>();
services.AddEmailDispatcherAsSingleton();
```

The singleton registration also registers the queue utility as singleton. Its `IEmailSender` dependency must be safe to capture and use for the application lifetime. Use `AddEmailDispatcherAsScoped()` with a scoped sender when dispatch behavior depends on request-scoped services.

Both registrations add the email utility and its Service Bus transmitter dependencies. An `IEmailSender` registration is still required even when queue routing is enabled because the dispatcher receives both routes through its constructor.

## Dispatch a message

```csharp
IEmailDispatcher dispatcher = serviceProvider.GetRequiredService<IEmailDispatcher>();

await dispatcher.Dispatch(message, cancellationToken);
```

With `UseQueue: true`, completion means the message was accepted by the configured Service Bus transmitter; delivery occurs later. With `UseQueue: false`, completion means the sender returned `true`. A `false` result from the sender becomes an `InvalidOperationException` so a failed direct send is not silently treated as success.

Cancellation stops pending work when the underlying route observes the token; it cannot retract a message already queued or sent.
