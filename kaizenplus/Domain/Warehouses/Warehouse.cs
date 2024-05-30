using kaizenplus.Domain.Countries;
using kaizenplus.Domain.WarehouseItems;
using System.Collections.Generic;
using System.Linq;

namespace kaizenplus.Domain.Warehouses
{
    public class Warehouse : FullyAuditedEntity<long>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public long CountryId { get; set; }
        public Country Country { get; set; }
        public List<WarehouseItem> WarehouseItem { get; set; }
    }
}
