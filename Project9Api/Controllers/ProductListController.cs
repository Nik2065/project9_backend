using Logic.Dto;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Project9Api.ApiDto;
using DataAccess;

namespace Project9Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductListController : ControllerBase
    {

        public ProductListController(IConfiguration config) 
        {
            _logger = NLog.LogManager.GetCurrentClassLogger();
            //пока база фейковая, но потом надо передать строку подключения
            var conn = config["MainSettings:DbConnectionString"];
            var db = new DataContext(conn);
            _logicForComputers = new Logic.LogicForComputers(db);

        }


        private readonly NLog.Logger _logger;
        private int _pageSize = 30;
        private Logic.LogicForComputers _logicForComputers;


        [HttpGet]
        [Route("[action]")]
        public ActionResult<GetProductsListResponse> GetProductsList(
            [FromQuery] int? pMin, 
            [FromQuery] int? pMax, [FromQuery] int? elementsInRow, int? page)
        {

            var result = new GetProductsListResponse();

            
            var pageSize = elementsInRow * 8;//пока берем такое значение
            if (page == null)
                page = 1;

            try
            {
                var options = new Logic.SearchOptions();
                options.PerfomanceFrom = pMin == (int?)null ? 0 : pMin;
                options.PerfomanceTo = pMax == (int?)null ? int.MaxValue : pMax;


                var a = _logicForComputers.SearchProducts(options);
                //todo: удалить
                var t = a.ToList();
                var paged = ListToPages<ProductDto>.GetPage(a, _pageSize, (int)page);

                result.Products = paged.PageItems.ToList();
                result.PageNumber = paged.PageNumber;
                result.PagesCount = paged.PagesCount;
                result.HasPreviousPage = paged.HasPreviousPage;
                result.HasNextPage = paged.HasNextPage;
            }
            catch (Exception ex) 
            {
                result.IsError = true;
                result.Message = ex.Message;
                _logger.Error(ex);
            }

            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public ActionResult Create()
        {
            try
            {

            }
            catch (Exception ex)
            {

            }

            return Ok(new { });
        }



    }
}
