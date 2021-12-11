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
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository _sqlService;
        private readonly IMapper _mapper;

        public AppointmentController(IAppointmentRepository dataAccessRepository, IMapper mapper)
        {
            _sqlService = dataAccessRepository;
            _mapper = mapper;
        }

        [HttpPost("{appointmentId}"), Authorize]
        public ActionResult<Appoinment> CreateAppointment(Appoinment appoinment)
        {
            appoinment.CreatedDate = DateTime.Now;
            _sqlService.SaveAppoinment(appoinment);
           
            return Created("Appoinment", appoinment);
        }
    }

}
