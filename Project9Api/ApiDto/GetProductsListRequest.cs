namespace Project9Api.ApiDto
{
    public class GetProductsListRequest
    {
        public SearchOptions SearchOptions { get; set; }
    }

    public class SearchOptions
    {
        public int CostMin { get; set; }
        public int CostMax { get; set; }


    }
}
