using EmployeeSkillsDevelopment.Infrastructure.Data;
using EmployeeSkillsDevelopment.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSkillsDevelopment.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IAppDbContext _appDbContext;
        private bool disposed = false;
        public UnitOfWork(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IUserRepository _userRepository => new UserRepository(_appDbContext);
        public IEmployeeRepository _employeeRepository => new EmployeeRepository(_appDbContext);

        public bool SaveChanges()
        {
            return _appDbContext.SaveChanges() > 0;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _appDbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        
    }
}
