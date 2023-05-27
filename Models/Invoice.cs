namespace Itroots_Task.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<InvoiceItem> Items { get; set; }
    }
}
