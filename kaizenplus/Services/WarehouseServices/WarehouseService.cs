using kaizenplus.DataAccess.UnitOfWorks;
using kaizenplus.Domain.Warehouses;
using kaizenplus.Models;
using kaizenplus.Security;
using kaizenplus.Services.WarehouseServices.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace kaizenplus.Services.WarehouseServices
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ISecurityManager securityManager;
        public WarehouseService(
            IUnitOfWork unitOfWork,
            ISecurityManager securityManager
         )
        {
            this.unitOfWork = unitOfWork;
            this.securityManager = securityManager;
        }

        public async Task<BaseResponse> Create(WarehouseInput input)
        {
            var userId = securityManager.GetUserId();
            var warehouse = new Warehouse
            {
                Name = input.Name,
                Address = input.Address,
                City = input.City,
                CountryId = input.CountryId,
                CreatedById = userId
            };
            unitOfWork.WarehouseRepository.Create(warehouse);
            await unitOfWork.SaveAsync();
            return new BaseResponse();

        }
        public async Task<BaseResponse> Delete(long Id)
        {
            var data = unitOfWork.WarehouseRepository.FirstOrDefault(x => x.Id == Id);
            if (data == null)
                return new BaseResponse(ErrorCode.NotFound);
            data.IsDeleted = true;
            unitOfWork.WarehouseRepository.Update(data);
            await unitOfWork.SaveAsync();
            return new BaseResponse();

        }
        public BaseResponse<WarehouseOutput> Get(long Id)
        {
            var data = unitOfWork.WarehouseRepository.FirstOrDefault(x => x.Id == Id && x.IsDeleted == false, query =>
            {
                return query.Include(x => x.Country).Include(x => x.WarehouseItem);
            });
            if (data == null)
                return new BaseResponse<WarehouseOutput>(null, ErrorCode.NotFound);

            return new BaseResponse<WarehouseOutput>(new WarehouseOutput(data));
        }
        public BaseResponse<PageOutput<WarehouseOutput>> Get(WarehouseSearch search)
        {
            search.PageNumber -= 1;
            int take = search.PageSize;
            if (search.Search == null)
                search.Search = "";
            int skip = search.PageSize == 0 ? search.PageNumber : search.PageNumber * search.PageSize;
            var data = unitOfWork.WarehouseRepository.Where(x => x.IsDeleted == false &&
            (x.Name.Contains(search.Search)
            || x.Address.Contains(search.Search)
            || x.City.Contains(search.Search))
            , query =>
            {
                return query.Include(x => x.Country).Include(x => x.WarehouseItem);
            }).OrderByDescending(x => x.WarehouseItem.Count());
            if (data == null)
                return new BaseResponse<PageOutput<WarehouseOutput>>(null, ErrorCode.NotFound);

            return new BaseResponse<PageOutput<WarehouseOutput>>(new PageOutput<WarehouseOutput>(data.Select(x => new WarehouseOutput(x)).Skip(skip).Take(take).ToList(), data.Count()));
        }
    }
}