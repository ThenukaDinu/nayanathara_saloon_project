using saloonAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Services
{
    public interface IReport
    {
        public List<Appoinment> GetAppointments(DateTime startDate, DateTime endDate);
    }
}
