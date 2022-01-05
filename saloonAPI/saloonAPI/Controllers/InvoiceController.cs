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
        private readonly IInvoiceRepository _sqlService; IAppointmentRepository appointmentRepository;
        private readonly IAppointmentRepository _sqlServiceAppoinment;
        private readonly IMapper _mapper;

        public InvoiceController(IInvoiceRepository dataAccessRepository,IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _sqlService = dataAccessRepository;
            _sqlServiceAppoinment = appointmentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IList<Invoice>> GetAllinvoices()
        {
            List<Invoice> invoices = _sqlService.GetAllInvoices();
            var vmInvoices = _mapper.Map<List<Invoice>>(invoices);
            return Ok(vmInvoices);
        }
        [HttpPost, Authorize]
        public ActionResult<Invoice> CreateInvoice(Invoice invoice)
        {

            var selectedAppoinment = _sqlServiceAppoinment.GetAppoinment(invoice.AppoinmentId);

            if (selectedAppoinment is not null && selectedAppoinment.Status == AppoinmentStatus.Completed)
            {
                invoice.CreatedDate = DateTime.Now;
                var result = _sqlService.SaveInvoice(invoice);

                if (result is not null)
                {
                    selectedAppoinment.Status = AppoinmentStatus.Paid;
                    _sqlServiceAppoinment.UpdateAppoinment(selectedAppoinment);

                    return Created("Invoice", invoice);
                }
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Something went wrong!" });
            }
            return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Please complete the appointment before make invoice!" });
        }

        [HttpPut("{invoiceId}"), Authorize]
        public IActionResult UpdateInvoice(int invoiceId, Invoice invoice)
        {
            var invoiceSelected = _sqlService.GetInvoice(invoiceId);

            if (invoiceSelected is null)
            {
                return NotFound();
            }
            invoiceSelected.InvoiceNo = invoice.InvoiceNo;
            invoiceSelected.AppoinmentId = invoice.AppoinmentId;
       
            _sqlService.UpdateInvoice(invoiceSelected);

            return NoContent();
        }

        [HttpDelete("{invoiceId}"), Authorize]
        public IActionResult DeleteInvoice(int invoiceId)
        {
            var selectedInvoice = _sqlService.GetInvoice(invoiceId);
            if (selectedInvoice is null)
            {
                return NotFound();
            }

            _sqlService.DeleteInvoice(selectedInvoice);

            return NoContent();
        }
    }

}
