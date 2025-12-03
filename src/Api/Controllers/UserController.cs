using Abyat.Api.Constants;
using Abyat.Api.Models;
using Abyat.Bl.Contracts.User;
using Abyat.Bl.Dtos.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Abyat.Api.Controllers;

[Route("api/v1/user")]
[ApiController]
[Authorize(Roles = SystemRoles.Admin)]
public class UserController(IUserCmd userCmdService) : ControllerBase
{
    /// <summary>
    /// Create a new admin.
    /// </summary>
    /// <param name="request">Admin registration data.</param>
    /// <returns>
    /// <see cref="IActionResult"/>  
    /// - 200 with success message if registration succeeds.  
    /// - 400 with validation or business errors if registration fails.  
    /// </returns>
    [HttpPost("create-admin")]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<string>>> CreateAdmin([FromBody] CreateAdminDto request)
    {
        if (request == null)
        {
            return BadRequest(ApiResponse<string>.FailResponse("Request body cannot be null."));
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ApiResponse<string>.FailResponse(
                "Validation failed",
                ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()));
        }

        UserResultDto result = await userCmdService.CreateAdminAsync(request);

        if (result.Success)
        {
            ApiResponse<string>? response = ApiResponse<string>.SuccessResponse("Please Confirm The Email");
            return Ok(response);
        }

        ApiResponse<string>? errorResponse = ApiResponse<string>.FailResponse("Registration failed", result.Errors);
        return BadRequest(errorResponse);
    }

}