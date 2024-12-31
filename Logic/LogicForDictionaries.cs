using DataAccess;
using Logic.Dto;

namespace Logic
{
    public class LogicForDictionaries
    {
        public LogicForDictionaries(DataContext db)
        {
            _db = db;
        }

        private DataContext _db;

        
        public List<CpuDto> GetCpuList()
        {
            var list = _db.DictionaryForCpuDb.ToList();

            var result = from item in list
                         select new CpuDto { Id=item.Id, Name = item.Name, PerformancePoints = item.PerformancePoints };

            return result.ToList();
        }


        public List<GpuDto> GetGpuList()
        {
            var list = _db.DictionaryForGpuDb.ToList();

            var result = from item in list
                         select new GpuDto { Id = item.Id, Name = item.Name, PerformancePoints = item.PerformancePoints };

            return result.ToList();
        }



    }
}
