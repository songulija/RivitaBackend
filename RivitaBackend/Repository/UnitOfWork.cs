﻿using RivitaBackend.IRepository;
using RivitaBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RivitaBackend.Repository
{
    // inherit from IUnitOfWork
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        private IGenericRepository<Transportation> _transportations;
        private IGenericRepository<Wagon> _wagons;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        // if transportations is null then provide new GenericRepository of type Transportations. providing context
        public IGenericRepository<Transportation> Transportations => _transportations ??= new GenericRepository<Transportation>(_context);

        public IGenericRepository<Wagon> Wagons => _wagons ??= new GenericRepository<Wagon>(_context);
        // garbage collector
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}