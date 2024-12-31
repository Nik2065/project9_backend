using DataAccess;
using DataAccess.Entities;
using Logic.Dto;
using System.Collections;

namespace Logic
{
    public class LogicForProducts
    {
        public LogicForProducts(DataContext db)
        {
            //todo: передавать строку подключения из настроек
            _db = db;
            _logger = NLog.LogManager.GetCurrentClassLogger();



        }

        private readonly DataContext _db;
        private NLog.Logger _logger;
        


        public IQueryable<ProductDto> SearchProducts(SearchOptions options)
        {
            //var result = new List<ComputerDto>();

            var cpuList = _db.DictionaryForCpuDb.AsQueryable();

            var tmp1 = cpuList.Count();

            if (options!= null)
            {
                if(options.PerfomanceFrom != null)
                {
                    cpuList = cpuList.Where(x => x.PerformancePoints > options.PerfomanceFrom);
                }
                if(options.PerfomanceTo != null)
                {
                    cpuList = cpuList.Where(x => x.PerformancePoints < options.PerfomanceTo);
                }
            }


            var gpuList = _db.DictionaryForGpuDb.AsQueryable();

            var tmp2 = cpuList.Count();

            var a = (from product in _db.Products
                     join cpu in cpuList on product.CpuId equals cpu.Id 
                     join gpu in gpuList on product.CpuId equals gpu.Id into tempGpu
                                                                        from gpu2 in tempGpu.DefaultIfEmpty()
                     select new ProductDto
                     {
                         Id = product.Id,
                         Title = product.ProductTitle,
                         Description = product.Description,
                         Price = product.CurrentPrice,

                         CpuPerfomanceClass = cpu.PerformanceClass,
                         CpuName = cpu.Name,
                         CpuPerfomancePoints = cpu.PerformancePoints,
                         GpuPerfomansClass = gpu2.PerformanceClass,
                         GpuName = gpu2.Name,
                         GpuPerfomansPoints = gpu2.PerformancePoints,
                     });



            var result = a.AsQueryable();
            return result;
        }

        public IQueryable<ProductDto> GetUserProductsList(int userId)
        {

            var cpuList = _db.DictionaryForCpuDb.AsQueryable();
            var gpuList = _db.DictionaryForGpuDb.AsQueryable();

            var userProducts = _db.Products.Where(x => x.AuthorId == userId);

            var a = (from product in userProducts
                     join cpu in cpuList on product.CpuId equals cpu.Id
                     join gpu in gpuList on product.CpuId equals gpu.Id into tempGpu
                     from gpu2 in tempGpu.DefaultIfEmpty()
                     select new ProductDto
                     {
                         Id = product.Id,
                         Title = product.ProductTitle,
                         Description = product.Description,
                         Price = product.CurrentPrice,
                         CpuPerfomanceClass = cpu.PerformanceClass,
                         CpuName = cpu.Name,
                         CpuPerfomancePoints = cpu.PerformancePoints,
                         GpuPerfomansClass = gpu2.PerformanceClass,
                         GpuName = gpu2.Name,
                         GpuPerfomansPoints = gpu2.PerformancePoints,
                     });



            var result = a.AsQueryable();
            return result;
        }

        public void AddProductToDb(ProductDb product)
        {

            _db.Products.Add(product);
            _db.SaveChanges();
        }


    }

    public enum ProductCategoryEnum
    {
        Computer = 0,
        MatherboardSet = 1
    }


    public class SearchOptions
    {
        public int? PerfomanceFrom { get; set; }
        public int? PerfomanceTo { get; set; }

        public int? ElementsInRow { get; set; }
        public int? Page { get; set; }//номер страницы
    }
}
