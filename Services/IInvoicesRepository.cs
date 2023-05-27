using Itroots_Task.Models;

namespace Itroots_Task.Services
{
    public interface IInvoicesRepository
    {
        Task AddInvoiceAsync(Invoice invoice);

        Task<List<Invoice>> GetAllInvoicesAsync();

        Task<Invoice> GetInvoiceByIdAsync(int id);
    }
}
