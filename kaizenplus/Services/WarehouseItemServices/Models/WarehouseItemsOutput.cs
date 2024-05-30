using kaizenplus.Attributes;
using kaizenplus.Domain.WarehouseItems;
using System.ComponentModel.DataAnnotations;

namespace kaizenplus.Services.WarehouseItemServices.Models
{
    public class WarehouseItemsOutput
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public long Quantity { get; set; }
        public decimal CostPrice { get; set; }
        public decimal? MSRPPrice { get; set; }
        public long WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public WarehouseItemsOutput(WarehouseItem output)
        {
            Id = output.Id;
            Name = output.Name;
            SKU = output.SKU;
            Quantity = output.Quantity;
            CostPrice = output.CostPrice;
            MSRPPrice = output.MSRPPrice;
            WarehouseId = output.WarehouseId;
            if (output.Warehouse != null)
            {
                WarehouseName = output.Warehouse.Name;
            }
        }

    }
}
