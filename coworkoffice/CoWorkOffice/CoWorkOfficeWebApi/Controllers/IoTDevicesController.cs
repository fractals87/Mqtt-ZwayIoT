using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoWorkOffice.Data;
using CoWorkOfficeModel.Models;
using CoWorkOfficeModel.Models.DTO;

namespace CoWorkOfficeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IoTDevicesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public IoTDevicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/IoTDevices
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<IotDeviceDTO>))]
        public async Task<ActionResult<IEnumerable<IotDeviceDTO>>> GetIoTDevices(int idGateway)
        {

            var listDeviceIoT = await _context.IoTDevices.Include(f=>f.Room).Where(f => f.Gateway_Id == idGateway).Select(s =>
                               new IotDeviceDTO
                               {
                                   description = s.Description,
                                   deviceType = s.DeviceType,
                                   probeType = s.ProbeType,
                                   registrationIdentifier = s.RegistrationIdentifier,
                                   room = s.Room.Description,
                               }).ToListAsync();
            return Ok(listDeviceIoT);

        }
    }
}
