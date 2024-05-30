using kaizenplus.DataAccess.UnitOfWorks;
using kaizenplus.Domain.WarehouseItems;
using kaizenplus.Models;
using kaizenplus.Security;
using kaizenplus.Services.WarehouseItemServices.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace kaizenplus.Services.WarehouseItemServices
{
    public class WarehouseItemService : IWarehouseItemService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ISecurityManager securityManager;
        public WarehouseItemService(
            IUnitOfWork unitOfWork,
            ISecurityManager securityManager
         )
        {
            this.unitOfWork = unitOfWork;
            this.securityManager = securityManager;
        }

        public async Task<BaseResponse> Create(WarehouseItemsInput input)
        {
            var userId = securityManager.GetUserId();
            var item = new WarehouseItem
            {
                Name = input.Name,
                Quantity = input.Quantity,
                CostPrice = input.CostPrice,
                MSRPPrice = input.MSRPPrice,
                SKU = input.SKU,
                WarehouseId = input.WarehouseId,
                CreatedById = userId
            };
            unitOfWork.WarehouseItemRepository.Create(item);
            await unitOfWork.SaveAsync();
            return new BaseResponse();

        }
        public async Task<BaseResponse> Delete(long Id)
        {
            var data = unitOfWork.WarehouseItemRepository.FirstOrDefault(x => x.Id == Id);
            if (data == null)
                return new BaseResponse(ErrorCode.NotFound);
            data.IsDeleted = true;
            unitOfWork.WarehouseItemRepository.Update(data);
            await unitOfWork.SaveAsync();
            return new BaseResponse();

        }
        public BaseResponse<WarehouseItemsOutput> Get(long Id)
        {
            var data = unitOfWork.WarehouseItemRepository.FirstOrDefault(x => x.Id == Id && x.IsDeleted == false, query => { return query.Include(x => x.Warehouse); });
            if (data == null)
                return new BaseResponse<WarehouseItemsOutput>(null, ErrorCode.NotFound);

            return new BaseResponse<WarehouseItemsOutput>(new WarehouseItemsOutput(data));
        }
        public BaseResponse<PageOutput<WarehouseItemsOutput>> Get(WarehouseItemSearch search)
        {
            if (search.Search == null)
                search.Search = "";
            search.PageNumber -= 1;
            int take = search.PageSize;
            int skip = search.PageSize == 0 ? search.PageNumber : search.PageNumber * search.PageSize;
            var data = unitOfWork.WarehouseItemRepository.Where(x => x.IsDeleted == false && x.Warehouse.IsDeleted == false &&
            (x.Name.Contains(search.Search)
            || x.SKU.Contains(search.Search)), query => { return query.Include(x => x.Warehouse); }).OrderByDescending(x => x.Quantity); ;
            if (data == null)
                return new BaseResponse<PageOutput<WarehouseItemsOutput>>(null, ErrorCode.NotFound);

            return new BaseResponse<PageOutput<WarehouseItemsOutput>>(new PageOutput<WarehouseItemsOutput>(data.Select(x => new WarehouseItemsOutput(x)).Skip(skip).Take(take).ToList(), data.Count()));
        }
    }
}
