using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using saloonAPI.Services;
using saloonAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace saloonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceRepository _sqlService;
        private readonly IMapper _mapper;

        public InvoiceController(IInvoiceRepository dataAccessRepository, IMapper mapper)
        {
            _sqlService = dataAccessRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IList<Invoice>> GetAllinvoices()
        {
            List<Invoice> invoices = _sqlService.GetAllInvoice();
            var vmInvoices = _mapper.Map<List<Invoice>>(invoices);
            return Ok(vmInvoices);
        }
        [HttpPost, Authorize]
        public ActionResult<Appoinment> CreateAppointment(Appoinment appoinment)
        {
            appoinment.CreatedDate = DateTime.Now;
            _sqlService.SaveAppoinment(appoinment);
           
            return Created("Appoinment", appoinment);
        }

        [HttpPut("{appointmentId}"), Authorize]
        public IActionResult UpdateAppointment(int appointmentId, Appoinment appoinment)
        {
            var appoinmentSelected = _sqlService.GetAppoinment(appointmentId);

            if (appoinmentSelected is null)
            {
                return NotFound();
            }
            else if (appoinmentSelected.Status != AppoinmentStatus.Created)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "This appointment cannot update!" });
            }

            appoinmentSelected.Type = appoinment.Type;
            appoinmentSelected.DurationInMins = appoinment.DurationInMins;
            appoinmentSelected.AppoinmentDate = appoinment.AppoinmentDate;
            appoinmentSelected.CreatedDate = appoinment.CreatedDate;

            _sqlService.UpdateAppoinment(appoinmentSelected);

            return NoContent();
        }

        [HttpPut("{appointmentId}/status-update"), Authorize]
        public IActionResult StatusUpdateAppointment(int appointmentId, Appoinment appoinment)
        {
            var appoinmentSelected = _sqlService.GetAppoinment(appointmentId);

            if (appoinmentSelected is null)
            {
                return NotFound();
            }
            else if (appoinmentSelected.Status == AppoinmentStatus.Completed)
            {
                appoinmentSelected.CompletedDate = DateTime.Now;
            }
            appoinmentSelected.Status = appoinment.Status;

            _sqlService.UpdateAppoinment(appoinmentSelected);

            return NoContent();
        }

        [HttpDelete("{appoinmentId}"), Authorize]
        public IActionResult DeleteAppoinment(int appoinmentId)
        {
            var selectedAppoinment = _sqlService.GetAppoinment(appoinmentId);
            if (selectedAppoinment is null)
            {
                return NotFound();
            }

            _sqlService.DeleteAppoinment(selectedAppoinment);

            return NoContent();
        }
    }

}
