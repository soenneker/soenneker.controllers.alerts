[![](https://img.shields.io/nuget/v/soenneker.controllers.alerts.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.controllers.alerts/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.controllers.alerts/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.controllers.alerts/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.controllers.alerts.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.controllers.alerts/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.controllers.alerts/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.controllers.alerts/actions/workflows/codeql.yml)

# Soenneker.Controllers.Alerts

Adds an MVC endpoint that accepts Azure Monitor common alert schema payloads and forwards them to `IAlertsCoordinator`.

## Install

```bash
dotnet add package Soenneker.Controllers.Alerts
```

## Register the controller and coordinator

```csharp
using Soenneker.Controllers.Alerts;
using Soenneker.Coordinators.Alerts.Registrars;

builder.Services
    .AddControllers()
    .AddApplicationPart(typeof(AlertsController).Assembly);

builder.Services.AddAlertsCoordinatorAsSingleton();
```

The coordinator requires these application settings in addition to the Microsoft Teams settings used by `Soenneker.MsTeams.Util`:

```json
{
  "Api": {
    "Alerts": {
      "AzureApiKey": "replace-with-a-random-secret"
    }
  },
  "Environment": "Production"
}
```

## Endpoint

```http
POST /alerts/azure?apiKey=<secret>
Content-Type: application/json
```

The body is a `CasRequest` from Azure Monitor's common alert schema.

| Response | Meaning |
| --- | --- |
| `201 Created` | The coordinator accepted and forwarded the alert |
| `400 Bad Request` | The payload did not contain the required alert data |

The controller is excluded from API Explorer and marked `AllowAnonymous`; the configured API key is its application-level credential. Authentication failure is surfaced by the coordinator through the application's exception handling.

## Security and operation

- Azure Monitor's ordinary webhook model commonly places a token in the callback URL. Query strings are frequently captured by gateways, access logs, traces, and deployment state. Use HTTPS, redact the `apiKey` query value throughout the request path, rotate it when exposed, and never place the callback URL in source control.
- For stronger authentication, prefer an Azure Monitor Secure Webhook protected by Microsoft Entra ID and an application endpoint configured for bearer authentication. That requires host-level authentication rather than this controller's static-key coordinator contract.
- Cancellation is forwarded to the coordinator and Microsoft Teams delivery. It does not undo a message already accepted by the downstream service.
- API version `1` is declared through ASP.NET API Versioning; the host determines whether versions are selected by query string, header, media type, or another configured reader.
