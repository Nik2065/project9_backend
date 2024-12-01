namespace Project9Api.ApiDto
{
    public class CreateComputerItemRequest
    {

        public string Name { get; set; }
        public string Description { get; set; }

        public int CpuId { get; set; }
        public int GpuId { get; set; }


    }
}
