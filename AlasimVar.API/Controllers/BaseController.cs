using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace AlasimVar.API.Controllers;

public class BaseController : ControllerBase
{
    public int CurrentUserId => Convert.ToInt32((User?.Identity as ClaimsIdentity)?.FindFirst("Id")?.Value);
}