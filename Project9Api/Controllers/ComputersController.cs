using DataAccess;
using DataAccess.Entities;
using Logic;
using Logic.Dto;
using Microsoft.AspNetCore.Mvc;
using Project9Api.ApiDto;


namespace Project9Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComputersController : ControllerBase
    {
        private readonly NLog.Logger _logger;
        private int _pageSize = 30;

        public ComputersController()
        {
            _logger = NLog.LogManager.GetCurrentClassLogger();
            
            //���� ���� ��������, �� ����� ���� �������� ������ �����������

            var db = new DataContext();

            _logicForComputers = new Logic.LogicForComputers(db);
        }

        //private void 




        private Logic.LogicForComputers _logicForComputers;


        /// <summary>
        ///  �������� ������ �����������
        /// </summary>
        /// <param name="pMin"></param>
        /// <param name="pMax"></param>
        /// <param name="elementsInRow"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public SearchComputersResponse SearchComputers(
            [FromQuery] int? pMin, [FromQuery] int? pMax, [FromQuery] int? elementsInRow, int? page)
        {

            var pageSize = elementsInRow * 8;//���� ����� ����� ��������

            //var db = new DataContext();
            //var list = db.DictionaryForCpuDb.ToList();
            var result = new SearchComputersResponse();

            if (page == null)
                page = 1;
            try
            {
                var options = new Logic.SearchOptions();
                options.PerfomanceFrom = pMin == (int?)null ? 0 : pMin;
                options.PerfomanceTo = pMax == (int?)null ? int.MaxValue : pMax;


                var a = _logicForComputers.SearchComputers(options);

                var paged = ListToPages<ComputerDto>.GetPage(a, _pageSize, (int)page);

                result.Computers = paged.PageItems.ToList();

                result.PageNumber = paged.PageNumber;
                result.PagesCount = paged.PagesCount;
                result.HasPreviousPage = paged.HasPreviousPage;
                result.HasNextPage = paged.HasNextPage;

                //
            }
            catch(Exception ex)
            {
                result.IsError = true;
                result.Message = ex.Message;
                _logger.Error(ex);
            }

            return result;
        }


        [Route("[action]")]
        //�������� ������ ��������
        [HttpGet]
        public IEnumerable<ComputerDto> GetProductDetails(int productId)
        {
            var result = new List<ComputerDto>();
            try
            {

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            return result;
        }


        [Route("[action]")]
        //�������� ������ ��������
        [HttpGet]
        public IEnumerable<ComputerDto> GetProductFullImages(int productId)
        {
            var result = new List<ComputerDto>();
            try
            {

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            return result;
        }



        [Route("[action]")]
        //�������� ������ ��������
        [HttpPost]
        public BaseResponse CreateComputerItem(CreateComputerItemRequest request)
        {
            var result = new BaseResponse { Message = "��������� ��������" };
            try
            {
                //TODO: ��������

                var c = new ComputerDb();
                c.CpuId = request.CpuId;
                c.GpuId = request.GpuId;
                c.Name = request.Name;
                c.Description = request.Description;

                //todo: ��������� �������

                _logicForComputers.AddComputerToDb(c);


            }
            catch(Exception ex)
            {
                result.IsError = true;
                result.Message = ex.Message;
                _logger.Error(ex);
            }

            return result;
                 
        }

    }
}
