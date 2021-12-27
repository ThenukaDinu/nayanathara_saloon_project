using saloonAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Services
{
    public interface IInvoiceRepository
    {
        public List<Invoice> GetAllInvoices();
        public Invoice SaveInvoice(Invoice appoinment);
        public void UpdateInvoice(Invoice appoinment);
        public void DeleteInvoice(Invoice appoinment);
        public Invoice GetInvoice(int appoinmentId);
    }
}
