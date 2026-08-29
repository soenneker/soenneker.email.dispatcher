[![](https://img.shields.io/nuget/v/soenneker.email.dispatcher.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.email.dispatcher/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.email.dispatcher/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.email.dispatcher/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.email.dispatcher.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.email.dispatcher/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.email.dispatcher/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.email.dispatcher/actions/workflows/codeql.yml)

# Soenneker.Email.Dispatcher

Defines a contract for dispatching email messages, handling routing logic (e.g., queuing or direct sending) based on configuration settings.

## Install

```bash
dotnet add package Soenneker.Email.Dispatcher
```

## Quick start

```csharp
using Soenneker.Email.Dispatcher.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddEmailDispatcherAsSingleton();
```

Adds `IEmailDispatcher` as a singleton service.

## What you get

- `IEmailDispatcher` — Defines a contract for dispatching email messages, handling routing logic (e.g., queuing or direct sending) based on configuration settings.
- `EmailDispatcherRegistrar` — Determines email dispatching/routing.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `IEmailDispatcher.Dispatch(emailMessage, cancellationToken)` | Dispatches the specified `emailMessage` for delivery. Depending on configuration, the message may be placed on a queue or sent immediately via the underlying email sender. | A `ValueTask` that represents the asynchronous dispatch operation. |
| `EmailDispatcherRegistrar.AddEmailDispatcherAsSingleton(services)` | Adds `IEmailDispatcher` as a singleton service. | The same service collection, so additional registrations can be chained. |
| `EmailDispatcherRegistrar.AddEmailDispatcherAsScoped(services)` | Adds `IEmailDispatcher` as a scoped service. | The same service collection, so additional registrations can be chained. |

## Important behavior

- `IEmailDispatcher.Dispatch(emailMessage, cancellationToken)`: Thrown if `emailMessage` is `null`. Thrown if the dispatcher is not properly configured or if sending fails due to misconfiguration.

## Practical notes

- Cancellation stops pending work; it does not undo work that has already completed.
