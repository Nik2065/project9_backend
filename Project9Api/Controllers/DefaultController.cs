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
    }
}
