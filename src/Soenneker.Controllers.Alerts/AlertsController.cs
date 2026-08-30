using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Soenneker.Controllers.Base;
using Soenneker.Coordinators.Alerts.Abstract;
using Soenneker.Requests.Azure.Alerts;

namespace Soenneker.Controllers.Alerts;

/// <summary>
/// Represents the alerts controller.
/// </summary>
[ApiExplorerSettings(IgnoreApi = true)]
[ApiController]
[ApiVersion("1")]
[Route("alerts")]
public sealed class AlertsController : BaseController
{
    private readonly IAlertsCoordinator _alertsCoord;

    public AlertsController(IAlertsCoordinator alertsCoord, IConfiguration config) : base(config)
    {
        _alertsCoord = alertsCoord;
    }

    /// <summary>
    /// Receives an Azure Monitor common-alert-schema payload and forwards it through the alerts coordinator.
    /// </summary>
    /// <param name="apiKey">Used for authentication to authorize the request.</param>
    /// <param name="request">Contains the details necessary for creating the Azure resource.</param>
    /// <param name="cancellationToken">Allows the operation to be canceled if needed.</param>
    /// <returns>A 201 response when the alert is accepted; otherwise, a 400 response when the payload cannot be processed.</returns>
    [MapToApiVersion("1")]
    [HttpPost("azure")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [AllowAnonymous]
    public async ValueTask<IActionResult> CreateAzure([FromQuery] string apiKey, [FromBody] CasRequest request, CancellationToken cancellationToken)
    {
        bool? accepted = await _alertsCoord.CreateAzure(apiKey, request, cancellationToken);

        return accepted == true ? StatusCode(StatusCodes.Status201Created) : BadRequest();
    }
}
