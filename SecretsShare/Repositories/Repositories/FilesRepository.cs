﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecretsShare.DTO;
using SecretsShare.HelperObject;
using SecretsShare.Repositories.Interfaces;

namespace SecretsShare.Repositories.Repositories
{
    public class FilesRepository: IFilesRepository
    {
        private readonly DataContext _context;
        private IFilesRepository _filesRepositoryImplementation;

        public FilesRepository(DataContext context)
        {
            _context = context;
        }

        public List<File> GetAll() => _context.Set<File>().ToList();

        public File GetById(Guid id) => _context.Set<File>().FirstOrDefault(f => f.Id == id);

        public async Task<Guid> Add(File file)
        {
            var result = await _context.Set<File>().AddAsync(file);
            await _context.SaveChangesAsync();
            return result.Entity.Id;
        }

        public async Task<Guid> Update(File entity)
        {
            _context.Files.Update(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public List<File> GetAllUserFiles(Guid userId)
        {
            return _context.Set<File>().Where(f => f.UserId == userId).ToList();
        }

        public void OnCascadeDelete(File file)
        {
            _context.Set<File>().Remove(file);
        }
    }
}