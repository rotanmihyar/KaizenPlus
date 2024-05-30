using kaizenplus.Domain.Warehouses;
using System.Linq;

namespace kaizenplus.Services.WarehouseServices.Models
{
    public class WarehouseOutput
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public long CountryId { get; set; }
        public string CountryName { get; set; }
        public long WarehouseItemsCount { get; set; }
        public WarehouseOutput(Warehouse output)
        {
            Id = output.Id;
            Name = output.Name;
            City = output.City;
            CountryId = output.CountryId;
            Address = output.Address;
            if (output.Country != null)
            {
                CountryName = output.Country.Name;
            }
            if (output.WarehouseItem != null)
            {
                WarehouseItemsCount = output.WarehouseItem.Where(x => x.IsDeleted == false).Count();
            }

        }
    }
}
