using Itroots_Task.Data;
using Itroots_Task.Models;
using Microsoft.EntityFrameworkCore;

namespace Itroots_Task.Services
{
    public class InvoicesRepository : IInvoicesRepository
    {
        private readonly AppDbContext _db;

        public InvoicesRepository(AppDbContext db) {
            _db = db;
        }

        public async Task AddInvoiceAsync(Invoice invoice)
        {
            await _db.Invoice.AddAsync(invoice);
            await _db.SaveChangesAsync();
        }

        

        public async Task<List<Invoice>> GetAllInvoicesAsync()
        {
            return await _db.Invoice.Include(i => i.Items).ToListAsync();
        }

        public async Task<Invoice> GetInvoiceByIdAsync(int id)
        {
            Invoice existingInvoice = await _db.Invoice.Include(i=>i.Items).FirstOrDefaultAsync(x => x.Id == id);

            return existingInvoice;
        }

    }
}
