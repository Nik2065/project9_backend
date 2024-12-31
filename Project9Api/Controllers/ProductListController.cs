using Logic.Dto;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Project9Api.ApiDto;
using DataAccess;
using Microsoft.AspNetCore.Authorization;


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
            _logicForProducts = new LogicForProducts(db);
            _userLogic = new UserLogic(db);

        }


        private readonly NLog.Logger _logger;
        private int _pageSize = 5;
        private LogicForProducts _logicForProducts;
        private UserLogic _userLogic;

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


                var a = _logicForProducts.SearchProducts(options);
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


        //
        [HttpPost]
        [Authorize]
        [Route("[action]")]
        public ActionResult<CreateProductResponse> CreateProduct(CreateProductRequest request)
        {
            var result = new CreateProductResponse { IsError = false, Message = "Объявление успешно добавлено" };

            try
            {
                var aid = _userLogic.GetClaimData(User.Claims).AccountId;

                var p = new DataAccess.Entities.ProductDb();
                p.CurrentPrice = request.Cost;
                p.Created = DateTime.Now;
                p.Description = request.Description;
                p.ProductTitle = request.Title;
                p.CpuId = request.CpuId;
                p.GpuId = request.GpuId;
                p.AuthorId = aid;
                p.CategoryId = (int)ProductCategoryEnum.Computer;

                _logicForProducts.AddProductToDb(p);

                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.Message = ex.Message;
                _logger.Error(ex);
            }


            return Ok(result);
        }


        [HttpGet]
        [Authorize]
        [Route("[action]")]
        public ActionResult<GetProductsListResponse> GetUserProductsList([FromQuery] int? page)
        {
            var result = new GetProductsListResponse { IsError = false, Message = "" };

            if (page == null)
                page = 1;

            try
            {
                var aid = _userLogic.GetClaimData(User.Claims).AccountId;

                var a = _logicForProducts.GetUserProductsList(aid);
                //todo: удалить
                var t = a.ToList();
                var paged = ListToPages<ProductDto>.GetPage(a, _pageSize, (int)page);

                result.Products = paged.PageItems.ToList();
                result.PageNumber = paged.PageNumber;
                result.PagesCount = paged.PagesCount;
                result.HasPreviousPage = paged.HasPreviousPage;
                result.HasNextPage = paged.HasNextPage;
            }
            catch(Exception ex)
            {
                result.IsError = true;
                result.Message = ex.Message;
                _logger.Error(ex);
            }

            return Ok(result);
        }
    }
}
