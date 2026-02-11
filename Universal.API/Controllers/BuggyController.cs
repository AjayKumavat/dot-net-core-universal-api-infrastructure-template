using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Universal.Shared.Responses;

namespace Universal.API.Controllers
{
    public class BuggyController : BaseApiController
    {
        [HttpGet]
        [Route("get-success")]
        public ApiResponse<string> GetSuccess()
        {
            return Response("Hello! working!");
        }

        [HttpGet]
        [Route("get-failure")]
        public ApiResponse<string> GetFailure()
        {
            return Response("failed");
        }

        [HttpGet("not-found")]
        public IActionResult GetNotFound()
        {
            return NotFound();
        }

        [HttpGet("bad-request")]
        public IActionResult GetBadRequest()
        {
            return BadRequest("This is not a good request.");
        }

        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorized()
        {
            return Unauthorized();
        }

        [HttpGet("validation-error")]
        public IActionResult GetValidationError()
        {
            ModelState.AddModelError("Problem1", "This is the first problem.");
            ModelState.AddModelError("Problem2", "This is the second problem.");
            return ValidationProblem();
        }

        [HttpGet("server-error")]
        public IActionResult GetServerError()
        {
            throw new Exception("This is a server error.");
        }

        [Authorize]
        [HttpGet("secure")]
        public ApiResponse<string> GetSecureData()
        {
            return Response("You are authenticated!");
        }

    }
}
