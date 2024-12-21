using Logic.Dto;

namespace Project9Api.ApiDto
{
    public class GetProductsListResponse : BaseResponse
    {
        public List<ProductDto> Products { get; set; }

        public int PageNumber { get; set; }
        public int PagesCount { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; } = false;

    }
}
