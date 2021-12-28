﻿using saloonAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Services
{
    public class SqlServiceInvoice : IInvoiceRepository
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        public List<Invoice> GetAllInvoices()
        {
            return _context.Invoices.ToList();
        }
        public Invoice GetInvoice(int invoiceId)
        {
            return _context.Invoices.FirstOrDefault(c => c.Id == invoiceId);
        }

        public Invoice SaveInvoice(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            _context.SaveChanges();

            return _context.Invoices.Find(invoice.Id);
        }

        public void UpdateInvoice(Invoice invoice)
        {
            _context.SaveChanges();
        }
        public void DeleteInvoice(Invoice invoice)
        {
            _context.Invoices.Remove(invoice);
            _context.SaveChanges();
        }
    }
}