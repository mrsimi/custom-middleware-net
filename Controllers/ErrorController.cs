using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Middleware.ResponseMessages;

namespace Middleware.Controllers
{
    [Route("api/error/")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet("401")]
        public ActionResult Return401Error()
        {
            var errorResponse = new ErrorResponse { Code = "E01", Msg = "Invalid client credentials, kindly check and try again" };

            return Unauthorized(errorResponse);
        }

    }
}