using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace Shared.ControllerBases;

public class CustomControllerBase : ControllerBase
{
    public IActionResult CreateActionResultInstance<T>(Response<T> response)
    {
        return new ObjectResult(response)
        {
            StatusCode = response.StatusCode
        };
    }
}