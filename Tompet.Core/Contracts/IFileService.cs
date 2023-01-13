namespace Tompet.Core.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Tompet.Infrastructure.Data;

    public interface IFileService
    {
        Task SaveFileAsync(ApplicationFile file);
    }
}
