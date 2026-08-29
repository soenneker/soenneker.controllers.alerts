[![](https://img.shields.io/nuget/v/soenneker.controllers.alerts.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.controllers.alerts/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.controllers.alerts/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.controllers.alerts/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.controllers.alerts.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.controllers.alerts/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.controllers.alerts/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.controllers.alerts/actions/workflows/codeql.yml)

# Soenneker.Controllers.Alerts

Represents the alerts controller.

## Install

```bash
dotnet add package Soenneker.Controllers.Alerts
```

## What you get

- `AlertsController` — Represents the alerts controller.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `AlertsController.CreateAzure(apiKey, request, cancellationToken)` | Handles the creation of Azure resources and returns a status code indicating the result of the operation. | Returns a 201 status code upon successful creation of the resource. |

## Practical notes

- Cancellation stops pending work; it does not undo work that has already completed.
