using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using saloonAPI.Models;
using saloonAPI.Services;
using saloonAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReport _sqlService;
        public ReportController(IReport dataAccessService)
        {
            _sqlService = dataAccessService;
        }

        [HttpPost, Authorize]
        public IActionResult AppointmentsByType(ReportsInput postData)
        {
            List<Appoinment> appoinments = _sqlService.GetAppointments(postData.StartDate, postData.EndDate);
            List<AppointmentByTypeVM> results = appoinments
                .GroupBy(a => a.Type)
                .Select(i => new AppointmentByTypeVM
                {
                    Type = i.First().Type,
                    TypeText = i.First().Type.ToString(),
                    Count = i.Count()
                }).ToList();
            return Ok(results);
        }
    }
}
