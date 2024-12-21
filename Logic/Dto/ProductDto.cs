namespace Logic.Dto
{
    public class ProductDto
    {
        public ProductDto()
        {
            ProductsTambprints = new List<ProductImageDto>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal CpuPerfomancePoints { get; set; }

        public string CpuName {  get; set; }

        public int CpuPerfomanceClass { get; set; }
        public decimal? GpuPerfomansPoints { get; set; }
        public string GpuName { get; set; }
        public int? GpuPerfomansClass { get; set; }
        
        //только мелкие варианты рисунков
        public List<ProductImageDto> ProductsTambprints { get; set; }   

    }

    public class ProductImageDto
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
    }
}
