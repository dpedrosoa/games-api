﻿using GamesAPI.Data.Interfaces;
using GamesAPI.Data.Repositories;
using GamesAPI.Models;

namespace GamesAPI.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        #region Repositories

        private IRepository<Game> _GameRepository;
        public IRepository<Game> GameRepository => _GameRepository ?? new Repository<Game>(_context);



        #endregion

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}