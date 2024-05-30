using kaizenplus.Attributes;
using kaizenplus.Enums;
using kaizenplus.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using kaizenplus.Services.WarehouseItemServices;
using kaizenplus.Services.WarehouseItemServices.Models;

namespace kaizenplus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseItemsController : ControllerBase
    {
        private readonly IWarehouseItemService service;

        public WarehouseItemsController(IWarehouseItemService service)
        {
            this.service = service;
        }

        [ValidateModel]
        [HttpPost]
        [AppAuthorize(Roles = new[] { Roles.Admin, Roles.Auditor, Roles.Management })]
        public async Task<BaseResponse> Create([FromForm] WarehouseItemsInput input)
        {
            return await service.Create(input);
        }
        [ValidateModel]
        [HttpGet("{id}")]
        [AppAuthorize(Roles = new[] { Roles.Admin, Roles.Auditor, Roles.Management })]
        public BaseResponse<WarehouseItemsOutput> Get(long id)
        {
            return service.Get(id);
        }
        [ValidateModel]
        [HttpPost("search")]
        [AppAuthorize(Roles = new[] { Roles.Admin, Roles.Auditor, Roles.Management })]
        public BaseResponse<PageOutput<WarehouseItemsOutput>> Get([FromForm] WarehouseItemSearch input)
        {
            return service.Get(input);
        }
        [ValidateModel]
        [HttpDelete("{id}")]
        [AppAuthorize(Roles = new[] { Roles.Admin, Roles.Auditor, Roles.Management })]
        public async Task<BaseResponse> Delete(long id)
        {
            return await service.Delete(id);
        }
    }
}
