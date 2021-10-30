using RivitaBackend.IRepository;
using RivitaBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RivitaBackend.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        //basically a register for every variation of Generic Repository
        IGenericRepository<Transportation> Transportations { get; }
        IGenericRepository<Wagon> Wagons { get; }
        IGenericRepository<Company> Companies { get; }
        // one more operation which is Save. to save all modified record at once
        Task Save();
    }
}
