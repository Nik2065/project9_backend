using Logic;
using Microsoft.AspNetCore.Mvc;
using Project9Api.ApiDto;

namespace Project9Api.Controllers
{
    /// <summary>
    /// Контроллер отдающий справочники
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class DataListController : Controller
    {
        public DataListController(IConfiguration config)
        {
            _logger = NLog.LogManager.GetCurrentClassLogger();

            var conn = config["MainSettings:DbConnectionString"];
            var db = new DataAccess.DataContext(conn);
            _logic = new LogicForDictionaries(db);
        }

        private NLog.Logger _logger;
        private LogicForDictionaries _logic;

        [HttpGet]
        [Route("[action]")]
        public ActionResult<GetCpuListResponse> GetCpuList()
        {
            var result = new GetCpuListResponse();

            try
            {
                result.CpuDtoList = _logic.GetCpuList();
            }
            catch (Exception ex)
            {
                _logger.Error(result);
                result.IsError = true;
                result.Message = ex.Message;
            }

            return Ok(result);

        }


        [HttpGet]
        [Route("[action]")]
        public ActionResult<GetGpuListResponse> GetGpuList()
        {
            var result = new GetGpuListResponse();

            try
            {
                result.GpuDtoList = _logic.GetGpuList();
            }
            catch (Exception ex)
            {
                _logger.Error(result);
                result.IsError = true;
                result.Message = ex.Message;
            }

            return Ok(result);

        }
    }
}
