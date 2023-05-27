using Microsoft.AspNetCore.Mvc;
using Itroots_Task.Models;
using Itroots_Task.Services;

namespace Itroots_Task.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly IInvoicesRepository _invoicesRepository;

        public InvoicesController(IInvoicesRepository invoicesRepository)
        {
            _invoicesRepository = invoicesRepository;
        }

        public async Task<IActionResult> Index()
        {
            List<Invoice> invoicesList = await _invoicesRepository.GetAllInvoicesAsync();

            return View(invoicesList);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await _invoicesRepository.GetAllInvoicesAsync() == null)
            {
                return View("NotFound");
            }

            Invoice invoice = await _invoicesRepository.GetInvoiceByIdAsync(id.Value);
            if (invoice == null)
            {
                return View("NotFound");
            }

            return View(invoice);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                await _invoicesRepository.AddInvoiceAsync(invoice);
                return RedirectToAction("Index");
            }
            return View(invoice);
        }
    }
}
