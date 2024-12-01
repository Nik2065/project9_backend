using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public  class ListToPages<T>
    {

        public static GetPageResult<T> GetPage(IQueryable<T> list, int pageSize, int pageNumber) 
        { 
            int itemsCount = list.Count();
            
            int pagesCount = 1;
            int currentPageNumber = 1;

            IQueryable<T> pageItems;

            if (itemsCount <= pageSize)
            {
                pageItems = list;
            }
            else
            {
                var d = (decimal)itemsCount/ pageSize;
                pagesCount = (int)Math.Ceiling(d);
                currentPageNumber = (pageNumber > pagesCount) ? pagesCount : pageNumber;

                var skipRecord = (currentPageNumber - 1) * pageSize;

                pageItems = list.Skip(skipRecord).Take(pageSize);
            }

            var result = new GetPageResult<T>
            {
                PageItems = pageItems,
                PagesCount = pagesCount,
                PageNumber = currentPageNumber,
                HasNextPage = currentPageNumber < pagesCount - 1,
                HasPreviousPage = currentPageNumber > 1
            };

            return result;
        
        }
    }


    public class GetPageResult<T>
    {
        public IQueryable<T> PageItems { get; set;}
        public int PageNumber { get; set;}
        public int PagesCount { get; set;}
        public bool HasNextPage { get; set;}
        public bool HasPreviousPage { get; set;} = false;
    }
}
