namespace Tompet.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Tompet.Core.Contracts;
    using Tompet.Infrastructure.Data;
    using Tompet.Infrastructure.Data.Repositories;

    public class FileService : IFileService
    {
        private readonly IApplicationDbRepository repo;

        public FileService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }
        public async Task SaveFileAsync(ApplicationFile file)
        {
            await repo.AddAsync(file);
            await repo.SaveChangesAsync();
        }
    }
}
