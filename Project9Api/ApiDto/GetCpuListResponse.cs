using Logic.Dto;

namespace Project9Api.ApiDto
{
    public class GetCpuListResponse: BaseResponse
    {

        public List<CpuDto> CpuDtoList { get; set; }
    }
}
