using kaizenplus.Attributes;
using kaizenplus.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using kaizenplus.Services.WarehouseServices;
using kaizenplus.Enums;
using kaizenplus.Services.WarehouseServices.Models;

namespace kaizenplus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService service;

        public WarehouseController(IWarehouseService service)
        {
            this.service = service;
        }

        [ValidateModel]
        [HttpPost]
        [AppAuthorize(Roles = new[] { Roles.Admin, Roles.Auditor, Roles.Management })]
        public async Task<BaseResponse> Create([FromForm] WarehouseInput input)
        {
            return await service.Create(input);
        }
        [ValidateModel]
        [HttpGet("{id}")]
        [AppAuthorize(Roles = new[] { Roles.Admin, Roles.Auditor, Roles.Management })]
        public BaseResponse<WarehouseOutput> Get(long id)
        {
            return service.Get(id);
        }
        [ValidateModel]
        [HttpPost("search")]
        [AppAuthorize(Roles = new[] { Roles.Admin, Roles.Auditor, Roles.Management })]
        public BaseResponse<PageOutput<WarehouseOutput>> Get([FromForm] WarehouseSearch input)
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
