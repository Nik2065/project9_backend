using Logic.Dto;

namespace Project9Api.ApiDto
{
    public class GetGpuListResponse: BaseResponse
    {
        public List<GpuDto> GpuDtoList { get; set; }
    }
}
