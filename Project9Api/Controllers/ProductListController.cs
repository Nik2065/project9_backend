using Microsoft.AspNetCore.Mvc;

namespace Project9Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductListController : ControllerBase
    {
        [HttpGet]
        [Route("[action]")]
        public ActionResult GetProductsList()
        {


            return Ok(new { });
        }



    }
}
