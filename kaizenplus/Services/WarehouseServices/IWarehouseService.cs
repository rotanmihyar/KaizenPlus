using kaizenplus.Models;
using kaizenplus.Services.WarehouseServices.Models;
using System.Threading.Tasks;

namespace kaizenplus.Services.WarehouseServices
{
    public interface IWarehouseService
    {
        Task<BaseResponse> Create(WarehouseInput input);
        BaseResponse<PageOutput<WarehouseOutput>> Get(WarehouseSearch search);
        BaseResponse<WarehouseOutput> Get(long Id);
        Task<BaseResponse> Delete(long Id);
    }
}
