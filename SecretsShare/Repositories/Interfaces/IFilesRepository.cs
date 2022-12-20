using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using SecretsShare.DTO;
using SecretsShare.HelperObject;

namespace SecretsShare.Repositories.Interfaces
{
    public interface IFilesRepository: IEfRepository<File>
    {
        public void OnCascadeDelete(File file);
        public void DeleteFile(File file);
    }
}