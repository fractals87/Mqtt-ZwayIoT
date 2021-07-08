using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoWorkOffice.Data;
using CoWorkOfficeModel.Models.DTO;
using Microsoft.AspNetCore.Authorization;

namespace CoWorkOfficeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class MeasuresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MeasuresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Measures
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MeasureDTO>))]
        public async Task<ActionResult<IEnumerable<MeasureDTO>>> GetMeasures(string? descrizione)
        {
            if (descrizione != null && descrizione!="undefined")
            {
                var list =  await _context.Measures
                .Include(f => f.IoTDevice)
                .ThenInclude(f => f.Room)
                .Where(f=>f.IoTDevice.Description.Contains(descrizione) || f.IoTDevice.Room.Description.Contains(descrizione) || f.DateTime.ToString().Contains(descrizione))
                .Select(s =>
                new MeasureDTO{
                    data = s.DateTime,
                    valore = s.Value,
                    room = s.IoTDevice.Room.Description,
                    iotdevice = s.IoTDevice.Description
                }).ToListAsync();
                return Ok(list.OrderByDescending(f=>f.data).ToList());
            }
            else
            {
                var list = await _context.Measures
                .Include(f => f.IoTDevice)
                .ThenInclude(f => f.Room)
                .OrderByDescending(f => f.DateTime)
                .Select(s =>
                new MeasureDTO{
                    data = s.DateTime,
                    valore = s.Value,
                    room = s.IoTDevice.Room.Description,
                    iotdevice = s.IoTDevice.Description
                }).OrderByDescending(f => f.data).ToListAsync();
                return Ok(list);
            }

        }

        
    }
}
