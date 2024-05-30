using kaizenplus.Attributes;
using System.ComponentModel.DataAnnotations;

namespace kaizenplus.Services.WarehouseItemServices.Models
{
    public class WarehouseItemsInput
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string SKU { get; set; }
        [GreaterThanZero]
        public long Quantity { get; set; }
        [Required]
        public decimal CostPrice { get; set; }
        public decimal? MSRPPrice { get; set; }
        [Required]
        public long WarehouseId { get; set; }
    }
}
