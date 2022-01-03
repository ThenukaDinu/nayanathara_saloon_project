using saloonAPI.Models;
using saloonAPI.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Services
{
    public class SqlServiceReport : IReport
    {
        private readonly ApplicationDbContext _context = new ();

        public List<Appoinment> GetAppointmentsByYear(int year)
        {
            return _context.Appoinments.Where(a => a.CreatedDate.Year == year).ToList();
        }

        public List<Appoinment> GetAppointmentsByYearRange(DateTime startDate, DateTime endDate)
        {
            return _context.Appoinments.Where(a => a.CreatedDate >= startDate && a.CreatedDate <= endDate.AddDays(1)).ToList();
        }

        public Invoice GetInvoiceForAppointment(int appointmentId)
        {
            return _context.Invoices.FirstOrDefault(i => i.AppoinmentId == appointmentId);
        }

        public List<ApplicationUser> GetUsersByYear(int year)
        {
            return _context.Users.Where(u => u.JoinDate.Year == year).ToList();
        }
    }
}
