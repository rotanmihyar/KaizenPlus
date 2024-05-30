using kaizenplus.Models;
using kaizenplus.Services.WarehouseItemServices.Models;
using System.Threading.Tasks;

namespace kaizenplus.Services.WarehouseItemServices
{
    public interface IWarehouseItemService
    {
        Task<BaseResponse> Create(WarehouseItemsInput input);
        Task<BaseResponse> Delete(long Id);
        BaseResponse<WarehouseItemsOutput> Get(long Id);
        BaseResponse<PageOutput<WarehouseItemsOutput>> Get(WarehouseItemSearch search);

    }
}
