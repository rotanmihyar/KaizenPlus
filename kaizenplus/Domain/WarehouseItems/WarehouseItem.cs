

using kaizenplus.Domain.Warehouses;

namespace kaizenplus.Domain.WarehouseItems
{
    public class WarehouseItem : FullyAuditedEntity<long>
    {

        public string Name { get; set; }
        public string SKU { get; set; }
        public long Quantity { get; set; }
        public decimal CostPrice { get; set; }
        public decimal? MSRPPrice { get; set; }
        public long WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}
