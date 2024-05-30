using kaizenplus.DataAccess.Repositories;
using kaizenplus.Domain.Users;
using kaizenplus.Domain.WarehouseItems;
using kaizenplus.Domain.Warehouses;
using System;
using System.Threading.Tasks;

namespace kaizenplus.DataAccess.UnitOfWorks
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly DatabaseService databaseService;
        public IRepository<WarehouseItem> WarehouseItemRepository { get; set; }
        public IRepository<Warehouse> WarehouseRepository { get; set; }
        public IRepository<User> UserRepository { get; set; }

        public UnitOfWork(DatabaseService databaseService, IRepository<User> UserRepository, IRepository<Warehouse> WarehouseRepository, IRepository<WarehouseItem> WarehouseItemRepository)
        {
            this.databaseService = databaseService;        
            this.UserRepository = UserRepository;
            this.WarehouseRepository = WarehouseRepository;
            this.WarehouseItemRepository = WarehouseItemRepository;
        }

        public void Save()
        {
            databaseService.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await databaseService.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    databaseService.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}