using Microsoft.AspNetCore.Mvc;

namespace Hozaru.WebApi.Controllers
{
    [ApiController]
    public class ErrorController : HozaruApiController
    {
        [Route("/error")]
        public IActionResult Error()
        {
            return Problem();
        }
    }
}
