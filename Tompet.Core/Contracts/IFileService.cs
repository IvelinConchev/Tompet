namespace Tompet.Core.Contracts
{
    using System.Threading.Tasks;
    using Tompet.Infrastructure.Data.Models;

    public interface IFileService
    {
        Task SaveFileAsync(ApplicationFile file);
    }
}
