# Azure Functions In-Memory State with Dependency Injection

This repository demonstrates how to use Dependency Injection (DI) to share
in-memory state between Azure Function executions within the same host instance.

## Overview

The solution includes:
- Two timer-triggered Azure Functions
- A shared in-memory message store
- Thread-safe state access using SemaphoreSlim
- DI-based singleton registration

This pattern enables lightweight coordination or session-like behavior
without requiring external storage such as Redis or SQL.

## Important Notes

- State is scoped to the Azure Functions host
- Data will be lost on host restarts, cold starts, or scale-out events
- This approach is not suitable for durable or distributed state

## Requirements

- .NET 10
- Azure Functions Core Tools
- Azure Functions isolated worker model

## Running Locally

1. Clone the repository
2. Run `func start`
3. Observe log output from TimerA and TimerB as they exchange messages

## Use Cases

- Lightweight coordination
- Host-local caching
- Proof-of-concept session handling

## License

MIT
