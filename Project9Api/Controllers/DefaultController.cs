using Microsoft.AspNetCore.Mvc;


namespace Project9Api.Controllers
{
    [Route("[controller]")]
    public class DefaultController : Controller
    {
        [HttpGet]
        [Route("/")]
        public string Index()
        {
            return "poniatno-api";
        }


        [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpGet]
        [Route("/auth")]
        public string Auth()
        {

            return "poniatno-api";
        }
    }
}
