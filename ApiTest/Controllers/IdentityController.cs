using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ApiTest.Controllers
{
    public class IdentityController : ControllerBase
    {
        [Authorize]
        [Route("/identity")]
        public IActionResult Get() => new JsonResult(from c in User.Claims select new { c.Type, c.Value });
    }
}
