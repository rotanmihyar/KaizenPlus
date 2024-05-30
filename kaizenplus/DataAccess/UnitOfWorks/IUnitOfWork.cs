using kaizenplus.DataAccess.Repositories;
using kaizenplus.Domain.Users;
using kaizenplus.Domain.WarehouseItems;
using kaizenplus.Domain.Warehouses;
using System.Threading.Tasks;

namespace kaizenplus.DataAccess.UnitOfWorks
{
    public interface IUnitOfWork
    {
        public IRepository<WarehouseItem> WarehouseItemRepository { get; set; }
        public IRepository<User> UserRepository { get; set; }
        public IRepository<Warehouse> WarehouseRepository { get; set; }
        Task SaveAsync();

        void Save();

        void Dispose();
    }
}