using saloonAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Services
{
    public class SqlServiceReport : IReport
    {
        private readonly ApplicationDbContext _context = new ();
        public List<Appoinment> GetAppointments(DateTime startDate, DateTime endDate)
        {
            return _context.Appoinments.Where(a => a.CreatedDate >= startDate && a.CreatedDate <= endDate.AddDays(1)).ToList();
        }
    }
}
